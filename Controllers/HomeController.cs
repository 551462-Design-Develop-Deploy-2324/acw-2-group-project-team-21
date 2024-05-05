using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ChatApp.Models;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        private static Dictionary<string, List<ChatMessage>> _chatRooms = new Dictionary<string, List<ChatMessage>>
        {
            ["lecture1"] = LoadMessagesFromFile("lecture1"),
            ["lab1"] = LoadMessagesFromFile("lab1"),
            ["oop"] = LoadMessagesFromFile("oop"),
            ["data_structures"] = LoadMessagesFromFile("data_structures"),
            ["algorithms"] = LoadMessagesFromFile("algorithms")
        };

        private static List<Feedback> _feedbackData = new List<Feedback>
        {
            new Feedback { Understanding = 4, Pace = 5, Comments = "Great lecture, very informative." },
            new Feedback { Understanding = 3, Pace = 4, Comments = "Good pace, but could explain certain topics better." },
            new Feedback { Understanding = 5, Pace = 4, Comments = "Excellent lecture overall." }
        };

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Perform user authentication (replace with actual authentication logic)
            if (username == "alice" && password == "password123")
            {
                // Redirect to appropriate dashboard based on user role
                return RedirectToAction("StudentDashboard");
            }
            else if (username == "bob" && password == "password456")
            {
                return RedirectToAction("SupervisorDashboard");
            }
            else if (username == "charlie" && password == "password789")
            {
                return RedirectToAction("LecturerDashboard");
            }

            ViewBag.Error = "Invalid username or password";
            return View("Index");
        }

        public IActionResult StudentDashboard()
        {
            return View();
        }

        public IActionResult SupervisorDashboard()
        {
            return View();
        }

        public IActionResult LecturerDashboard()
        {
            var viewModel = new FeedbackDataViewModel
            {
                FeedbackItems = _feedbackData.Select(f => new FeedbackViewModel
                {
                    Understanding = f.Understanding,
                    Pace = f.Pace,
                    Comments = f.Comments
                }).ToList()
            };

            return View(viewModel);
        }

        public IActionResult Room(string roomName)
        {
            var messages = _chatRooms.GetValueOrDefault(roomName) ?? new List<ChatMessage>();
            ViewBag.RoomName = roomName;
            ViewBag.Messages = messages;
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(string roomName, string message)
        {
            var messages = _chatRooms.GetValueOrDefault(roomName) ?? new List<ChatMessage>();
            var newMessage = new ChatMessage
            {
                Username = "User", // Replace with authenticated user's username
                Message = message,
                Timestamp = DateTime.Now
            };

            messages.Add(newMessage);
            _chatRooms[roomName] = messages;

            AppendMessageToFile(roomName, newMessage);

            return RedirectToAction("Room", new { roomName });
        }

        [HttpPost]
        public IActionResult Reply(string roomName, string message, int messageId)
        {
            var messages = _chatRooms.GetValueOrDefault(roomName) ?? new List<ChatMessage>();
            var replyMessage = new ChatMessage
            {
                Username = "User", // Replace with authenticated user's username
                Message = message,
                Timestamp = DateTime.Now,
                ReplyToId = messageId
            };

            messages.Add(replyMessage);
            _chatRooms[roomName] = messages;

            AppendMessageToFile(roomName, replyMessage);

            return RedirectToAction("Room", new { roomName });
        }

        [HttpPost]
        public IActionResult SubmitFeedback(int understanding, int pace, string comments)
        {
            var feedback = new Feedback
            {
                Understanding = understanding,
                Pace = pace,
                Comments = comments
            };

            _feedbackData.Add(feedback);
            AppendFeedbackToFile(feedback);

            return RedirectToAction("StudentDashboard");
        }

        private static List<ChatMessage> LoadMessagesFromFile(string roomName)
        {
            var filePath = $"{roomName}.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                return lines.Select(line => ChatMessage.FromString(line)).ToList();
            }
            return new List<ChatMessage>();
        }

        private static void AppendMessageToFile(string roomName, ChatMessage message)
        {
            var filePath = $"{roomName}.txt";
            var messageString = message.ToString();
            System.IO.File.AppendAllLines(filePath, new[] { messageString });
        }

        private static void AppendFeedbackToFile(Feedback feedback)
        {
            var filePath = "feedback.txt";
            var feedbackString = $"{feedback.Understanding},{feedback.Pace},{feedback.Comments}";
            System.IO.File.AppendAllLines(filePath, new[] { feedbackString });
        }
    }
}
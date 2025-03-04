using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers
{
    [Route("")]
    [ApiController]
    public class GreenSphereController : ControllerBase
    {
        [HttpGet]
        public ContentResult Get()
        {
            const string htmlContent = @"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>GreenSphere Water Agriculture API</title>
                    <style>
                        body {
                            margin: 0;
                            padding: 0;
                            height: 100vh;
                            background: #0a2a3b; /* Deep blue for water theme */
                            display: flex;
                            flex-direction: column;
                            justify-content: center;
                            align-items: center;
                            font-family: Arial, sans-serif;
                            overflow: hidden;
                        }

                        .container {
                            position: relative;
                            text-align: center;
                            color: white;
                        }

                        .shape1 {
                            position: absolute;
                            top: 20%;
                            left: 10%;
                            width: 300px;
                            height: 300px;
                            background: #2e7d32; /* Green for agriculture/plant life */
                            border-radius: 50%;
                            opacity: 0.7;
                            z-index: 1;
                            animation: float 4s ease-in-out infinite;
                        }

                        .shape2 {
                            position: absolute;
                            top: 60%;
                            right: 10%;
                            width: 200px;
                            height: 200px;
                            background: #0288d1; /* Light blue for water */
                            border-radius: 50%;
                            opacity: 0.6;
                            z-index: 1;
                            animation: rotate 6s linear infinite;
                        }

                        .shape3 {
                            position: absolute;
                            bottom: 10%;
                            left: 20%;
                            width: 150px;
                            height: 150px;
                            background: #43a047; /* Another green shade for plants */
                            border-radius: 50%;
                            opacity: 0.5;
                            z-index: 1;
                            animation: float 5s ease-in-out infinite;
                        }

                        h1 {
                            font-size: 2.5em;
                            margin-bottom: 20px;
                            z-index: 2;
                            position: relative;
                            transition: color 0.3s ease;
                        }

                        h1:hover {
                            color: #a5d6a7; /* Light green hover effect for agriculture theme */
                        }

                        .join-button {
                            background-color: #0288d1; /* Blue for water theme */
                            color: white;
                            padding: 15px 30px;
                            border: none;
                            border-radius: 5px;
                            font-size: 1.2em;
                            cursor: pointer;
                            text-decoration: none;
                            z-index: 2;
                            position: relative;
                            transition: background-color 0.3s ease;
                        }

                        .join-button:hover {
                            background-color: #01579b; /* Darker blue on hover */
                        }

                        .footer {
                            position: absolute;
                            bottom: 20px;
                            font-size: 0.8em;
                            color: #a2a2a2;
                            z-index: 2;
                        }

                        .created-by {
                            position: absolute;
                            bottom: 10px;
                            right: 20px;
                            font-size: 0.8em;
                            color: #a2a2a2;
                            z-index: 2;
                        }

                        @keyframes float {
                            0%, 100% { transform: translateY(0); }
                            50% { transform: translateY(-20px); }
                        }

                        @keyframes rotate {
                            0% { transform: rotate(0deg); }
                            100% { transform: rotate(360deg); }
                        }
                    </style>
                    <script>
                        // Animation for shapes on load
                        document.addEventListener('DOMContentLoaded', () => {
                            const shapes = document.querySelectorAll('.shape1, .shape2, .shape3');
                            shapes.forEach(shape => {
                                shape.style.transition = 'opacity 0.5s ease';
                                shape.style.opacity = '0';
                                setTimeout(() => shape.style.opacity = '0.7', 500); // Fade in effect
                            });

                            // Button click behavior (redirect to a contact page or show alert)
                            const button = document.querySelector('.join-button');
                            button.addEventListener('click', () => {
                                // Redirect to a hypothetical contact page or show a message
                                window.location.href = 'https://greensphere-api.runasp.net/contact'; // Replace with actual URL
                                // Alternatively, you can use an alert or modal:
                                // alert('Thank you for joining GreenSphere Water Agriculture!');
                            });

                            // Add subtle sparkle effect to shapes
                            function createSparkle(shape) {
                                const sparkle = document.createElement('div');
                                sparkle.style.position = 'absolute';
                                sparkle.style.width = '5px';
                                sparkle.style.height = '5px';
                                sparkle.style.background = 'white';
                                sparkle.style.borderRadius = '50%';
                                sparkle.style.opacity = '0.8';
                                sparkle.style.animation = 'sparkle 1s ease-out';
                                const rect = shape.getBoundingClientRect();
                                sparkle.style.left = Math.random() * rect.width + 'px';
                                sparkle.style.top = Math.random() * rect.height + 'px';
                                shape.appendChild(sparkle);

                                setTimeout(() => sparkle.remove(), 1000);
                            }

                            shapes.forEach(shape => {
                                setInterval(() => createSparkle(shape), 2000); // Sparkle every 2 seconds
                            });
                        });

                        // Sparkle animation
                        @keyframes sparkle {
                            0% { opacity: 0.8; transform: scale(1); }
                            100% { opacity: 0; transform: scale(2); }
                        }
                    </script>
                </head>
                <body>
                    <div class=""container"">
                        <div class=""shape1""></div>
                        <div class=""shape2""></div>
                        <div class=""shape3""></div>
                        <h1>GreenSphere Water Agriculture API</h1>
                        <a href=""javascript:void(0)"" class=""join-button"">Join Us Now</a>
                        <div class=""footer"">© 2025 GreenSphere, all rights reserved</div>
                        <div class=""created-by"">Created By Mohamed ElHelaly</div>
                    </div>
                </body>
                </html>";

            return new ContentResult
            {
                Content = htmlContent,
                ContentType = "text/html",
                StatusCode = 200
            };
        }
    }
}
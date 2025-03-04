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
                    <link href=""https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css"" rel=""stylesheet"">
                    <style>
                        :root {
                            --deep-blue: #0a2a3b;
                            --accent-green: #2e7d32;
                            --water-blue: #0288d1;
                            --light-green: #43a047;
                            --text-white: #ffffff;
                        }

                        * {
                            box-sizing: border-box;
                            margin: 0;
                            padding: 0;
                        }

                        body {
                            font-family: 'Arial', sans-serif;
                            background: linear-gradient(135deg, var(--deep-blue), #0c3c57);
                            color: var(--text-white);
                            min-height: 100vh;
                            display: flex;
                            flex-direction: column;
                            overflow-x: hidden;
                            perspective: 1000px;
                        }

                        .header {
                            background: rgba(14, 41, 59, 0.9);
                            padding: 20px;
                            text-align: center;
                            position: relative;
                            overflow: hidden;
                        }

                        .header::before {
                            content: '';
                            position: absolute;
                            top: 0;
                            left: 0;
                            right: 0;
                            bottom: 0;
                            background: linear-gradient(45deg, transparent, rgba(255,255,255,0.1), transparent);
                            animation: wave 10s linear infinite;
                        }

                        h1 {
                            font-size: 3em;
                            position: relative;
                            text-shadow: 0 0 10px rgba(165, 214, 167, 0.7);
                            transition: all 0.3s ease;
                        }

                        h1:hover {
                            transform: scale(1.05) rotate(1deg);
                            text-shadow: 0 0 20px rgba(165, 214, 167, 0.9);
                        }

                        .container {
                            flex-grow: 1;
                            display: flex;
                            flex-direction: column;
                            justify-content: center;
                            align-items: center;
                            position: relative;
                            padding: 20px;
                        }

                        .shape-container {
                            position: relative;
                            width: 100%;
                            height: 400px;
                        }

                        .shape {
                            position: absolute;
                            border-radius: 50%;
                            opacity: 0.6;
                            transition: all 0.5s ease;
                        }

                        .shape1 {
                            background: var(--accent-green);
                            width: 300px;
                            height: 300px;
                            top: 20%;
                            left: 10%;
                            animation: float 4s ease-in-out infinite;
                        }

                        .shape2 {
                            background: var(--water-blue);
                            width: 250px;
                            height: 250px;
                            top: 50%;
                            right: 10%;
                            animation: rotate 6s linear infinite;
                        }

                        .shape3 {
                            background: var(--light-green);
                            width: 200px;
                            height: 200px;
                            bottom: 10%;
                            left: 20%;
                            animation: float 5s ease-in-out infinite alternate;
                        }

                        .action-buttons {
                            display: flex;
                            justify-content: center;
                            margin-top: 30px;
                        }

                        .btn {
                            padding: 12px 25px;
                            border: none;
                            border-radius: 25px;
                            font-size: 1em;
                            cursor: pointer;
                            transition: all 0.3s ease;
                            display: flex;
                            align-items: center;
                            gap: 10px;
                            transform-style: preserve-3d;
                        }

                        .btn-docs {
                            background-color: var(--water-blue);
                            color: white;
                        }

                        .btn-docs:hover {
                            background-color: #02a2f0;
                            transform: translateZ(20px) rotate(3deg);
                        }

                        .social-links {
                            display: flex;
                            justify-content: center;
                            gap: 20px;
                            margin-top: 20px;
                        }

                        .social-link {
                            color: var(--text-white);
                            font-size: 2em;
                            transition: transform 0.3s ease, color 0.3s ease;
                        }

                        .social-link:hover {
                            transform: scale(1.2);
                        }

                        .social-link.github:hover {
                            color: #4078c0;
                        }

                        .social-link.linkedin:hover {
                            color: #0077B5;
                        }

                        .social-link.facebook:hover {
                            color: #3b5998;
                        }

                        .footer {
                            background: rgba(14, 41, 59, 0.9);
                            padding: 20px;
                            text-align: center;
                        }

                        @keyframes float {
                            0%, 100% { transform: translateY(0); }
                            50% { transform: translateY(-30px); }
                        }

                        @keyframes rotate {
                            0% { transform: rotate(0deg); }
                            100% { transform: rotate(360deg); }
                        }

                        @keyframes wave {
                            0% { transform: skewX(-5deg); }
                            50% { transform: skewX(5deg); }
                            100% { transform: skewX(-5deg); }
                        }

                        @media (max-width: 768px) {
                            .shape-container {
                                height: 250px;
                            }
                            .shape {
                                width: 150px !important;
                                height: 150px !important;
                            }
                            .action-buttons {
                                flex-direction: column;
                                align-items: center;
                            }
                            .social-links {
                                flex-wrap: wrap;
                            }
                        }
                    </style>
                </head>
                <body>
                    <div class=""header"">
                        <h1>GreenSphere Water Agriculture API</h1>
                    </div>
                    
                    <div class=""container"">
                        <div class=""shape-container"">
                            <div class=""shape shape1""></div>
                            <div class=""shape shape2""></div>
                            <div class=""shape shape3""></div>
                        </div>

                        <div class=""action-buttons"">
                            <button class=""btn btn-docs"" onclick=""window.open('https://greensphere-api.runasp.net/swagger/index.html', '_blank')"">
                                <i class=""fas fa-file-alt""></i> View Documentation
                            </button>
                        </div>

                        <div class=""social-links"">
                            <a href=""https://github.com/NetNinjaEngineer"" target=""_blank"" class=""social-link github"">
                                <i class=""fab fa-github""></i>
                            </a>
                            <a href=""https://www.linkedin.com/in/mohamed-ehab-elhelaly"" target=""_blank"" class=""social-link linkedin"">
                                <i class=""fab fa-linkedin""></i>
                            </a>
                            <a href=""https://www.facebook.com/mohamed.elhelaly.50951"" target=""_blank"" class=""social-link facebook"">
                                <i class=""fab fa-facebook""></i>
                            </a>
                        </div>
                    </div>

                    <div class=""footer"">
                        <div>© 2025 GreenSphere, all rights reserved</div>
                        <div class=""created-by"">Created By Mohamed ElHelaly</div>
                    </div>

                    <script>
                        document.addEventListener('DOMContentLoaded', () => {
                            const shapes = document.querySelectorAll('.shape');
                            
                            // Sparkle effect
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
                                sparkle.style.left = `${Math.random() * rect.width}px`;
                                sparkle.style.top = `${Math.random() * rect.height}px`;
                                shape.appendChild(sparkle);

                                setTimeout(() => sparkle.remove(), 1000);
                            }

                            // Periodic sparkles
                            shapes.forEach(shape => {
                                setInterval(() => createSparkle(shape), 2000);
                            });

                            // Interactive hover effects
                            shapes.forEach(shape => {
                                shape.addEventListener('mouseenter', () => {
                                    shape.style.transform = 'scale(1.1)';
                                    shape.style.opacity = '0.8';
                                });
                                shape.addEventListener('mouseleave', () => {
                                    shape.style.transform = 'scale(1)';
                                    shape.style.opacity = '0.6';
                                });
                            });
                        });
                    </script>
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
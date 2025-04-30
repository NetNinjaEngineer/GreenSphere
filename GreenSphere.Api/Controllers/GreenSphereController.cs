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
                            --primary-blue: #0a2a3b;
                            --accent-green: #2e7d32;
                            --text-white: #ffffff;
                        }

                        * {
                            box-sizing: border-box;
                            margin: 0;
                            padding: 0;
                        }

                        body {
                            font-family: 'Arial', sans-serif;
                            background: linear-gradient(135deg, var(--primary-blue), #155778);
                            color: var(--text-white);
                            min-height: 100vh;
                            display: flex;
                            flex-direction: column;
                        }

                        .header {
                            background: rgba(14, 41, 59, 0.9);
                            padding: 1.5rem;
                            text-align: center;
                            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                        }

                        h1 {
                            font-size: 2.5rem;
                            margin-bottom: 0.5rem;
                        }

                        .container {
                            flex-grow: 1;
                            display: flex;
                            flex-direction: column;
                            justify-content: center;
                            align-items: center;
                            padding: 2rem;
                            text-align: center;
                        }

                        .api-info {
                            margin-bottom: 2rem;
                            max-width: 800px;
                        }

                        .api-info p {
                            font-size: 1.2rem;
                            margin-bottom: 1rem;
                            line-height: 1.6;
                        }

                        .logo {
                            width: 120px;
                            height: 120px;
                            background-color: var(--accent-green);
                            border-radius: 50%;
                            margin: 0 auto 2rem;
                            display: flex;
                            align-items: center;
                            justify-content: center;
                        }

                        .logo i {
                            font-size: 3rem;
                            color: white;
                        }

                        .btn {
                            padding: 0.8rem 1.5rem;
                            border: none;
                            border-radius: 4px;
                            font-size: 1rem;
                            cursor: pointer;
                            transition: all 0.2s ease;
                            display: inline-flex;
                            align-items: center;
                            gap: 0.5rem;
                            font-weight: bold;
                            text-decoration: none;
                            margin: 0.5rem;
                        }

                        .btn-docs {
                            background-color: var(--accent-green);
                            color: white;
                        }

                        .btn-docs:hover {
                            background-color: #43a047;
                            transform: translateY(-2px);
                        }

                        .social-links {
                            display: flex;
                            justify-content: center;
                            gap: 1.5rem;
                            margin-top: 2rem;
                        }

                        .social-link {
                            color: var(--text-white);
                            font-size: 1.5rem;
                            transition: transform 0.2s ease, color 0.2s ease;
                        }

                        .social-link:hover {
                            transform: scale(1.1);
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
                            padding: 1rem;
                            text-align: center;
                        }

                        .footer div {
                            margin: 0.3rem 0;
                        }

                        @media (max-width: 768px) {
                            h1 {
                                font-size: 2rem;
                            }

                            .api-info p {
                                font-size: 1rem;
                            }
                        }
                    </style>
                </head>

                <body>
                    <div class=""header"">
                        <h1>GreenSphere Water Agriculture API</h1>
                    </div>

                    <div class=""container"">
                        <div class=""logo"">
                            <i class=""fas fa-leaf""></i>
                        </div>

                        <div class=""api-info"">
                            <p>Access sustainable water management solutions for agriculture through our comprehensive API.</p>
                        </div>

                        <a href=""https://greensphere-api.runasp.net/swagger/index.html"" target=""_blank"" class=""btn btn-docs"">
                            <i class=""fas fa-file-alt""></i> View API Documentation
                        </a>

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
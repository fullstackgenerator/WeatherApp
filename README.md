### WeatherApp

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

<p align="center" style="max-width: 700px; margin: 0 auto;"> <strong>WeatherApp</strong> is a lightweight weather application built using ASP.NET Core MVC. It allows users to search for any city and displays the current weather conditions along with a 5-day forecast, powered by WeatherAPI.com.</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/02587442-3911-4b35-b51f-18b2a4395396" alt="Main weather view" style="max-width: 100%; height: auto;" />
</p>

<p align="center" style="max-width: 700px; margin: 0 auto;">Under the hood, the app makes HTTP requests to WeatherAPI’s endpoints for both current conditions and extended forecasts. JSON responses are deserialized using `Newtonsoft.Json` and mapped into clean model classes like `WeatherModel` and `ForecastDay`.</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/4f5cc25b-e770-41d0-bff5-1e3adbab3225" alt="Code sample" style="max-width: 100%; height: auto;" />
</p>

<p align="center" style="max-width: 700px; margin: 0 auto;">The UI is styled with Bootstrap and includes a responsive layout, dark mode toggle, and weather icons pulled from the API itself.</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/17d8cc94-ce42-4b2e-912d-e64ba4aac073" alt="Mobile view" style="max-width: 100%; height: auto;" />
</p>

### License

This project is licensed under the [MIT License](LICENSE).

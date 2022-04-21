# LocalWeather

This is a project I started working on around mid-July in the summer of 2020- a desktop application, written in C#, that can fetch the current weather data for either your location or
a city/postal code of your choice. I wrote it to practice a few major skills, including general C# programming, web API use, and UI design using WPF.

Since I ended up later doing pretty much everything attempted here as part of my regular academic work, this project has more or less been abandoned. However, I've decided to at least make a few security-oriented changes and push my work to Github, so others can at least see what I've been working on at this point in time.

### Current features:

- Uses GeoCoordinateWatcher to acquire the user's current location.
- Fetches current, accurate weather data via the [OpenWeatherMap API](https://openweathermap.org/).
- Acquires the weather for either the user's current location, or a given city or postal code.
- Listed data includes a general description, current, high, and low temperatures, wind speed and direction, cloud cover, and rainfall.

### Upcoming features:

- A UI that will allow users to actually take advantage of the functionality listed above.
- Better handling for potential errors and delays associated with GeoCoordinateWatcher and API usage.
- Weather forecasts? More detailed information? Better visuals?

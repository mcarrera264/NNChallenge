# NNChallenge - C# App Developer Challenge

## Overview
This is my implementation for the C# App Developer Challenge.  
The goal was to create a multiplatform native temperature forecast app for Android and iOS using the provided template, without Xamarin.Forms or .NET MAUI.

The app allows users to:
- Select a city (Berlin, Moscow, New York, Tokyo)
- View the hourly weather forecast for the next 3 days, displaying temperature in Celsius and Fahrenheit.

---

## Features implemented

### Android
- Fully implemented and tested:
  - Native UI (MainActivity + ForecastActivity)
  - City selection and transition
  - API consumption using WeatherAPI.com
  - Display of hourly forecast data in a ListView
  - Clear architecture using shared code (WeatherService, WeatherForecastVO)

### iOS
- ForecastViewController implemented:
  - Follows same architecture as Android
  - Uses shared WeatherService
  - UI with UITableView displaying hourly forecast

Note:  
Due to the lack of access to a Mac and Xcode at this time, I was not able to test the iOS implementation, but the structure is prepared to minimize code duplication and allow easy integration.

---

## Shared code structure
- WeatherService:
  - API call to WeatherAPI
  - JSON parsing
  - Returns objects conforming to IWeatherForcastVO and IHourWeatherForecastVO

- WeatherForecastVO:
  - Implements required interfaces
  - Ensures reusable, platform-independent models

---

## API Used
https://api.weatherapi.com/v1/forecast.json?key=898147f83a734b7dbaa95705211612&q={city}&days=3&aqi=no&alerts=no

---

## How to build and run

### Android
- Open solution `NNChallenge.sln` in Visual Studio 2022
- Ensure `NNChallenge.Droid` is set as Startup Project
- Select an Android device or emulator
- Build and Run (F5 or Ctrl+F5)

### iOS
- Requires Mac + Xcode with Xamarin.iOS configured
- ForecastViewController is prepared for integration

---

## Notes for reviewers
- The iOS part was developed with care to follow the same structure and architecture but could not be tested due to environment limitations.
- Android part fully working and tested.

---

Thank you for reviewing my submission.  
If you need any clarifications or further details, I will be happy to provide them.

Kind regards,  
Marcel Carrera

# Cuicocha VR Experience

A virtual reality project developed in Unity using Google Cardboard to create an immersive experience.

## 📱 Description

This project implements a virtual reality experience using Google Cardboard, allowing users to explore 3D content through mobile devices with VR support.

## 🚀 Features

- **Google Cardboard Compatibility**: Optimized for mobile devices with VR support
- **Immersive Experience**: Interactive 3D content
- **Adaptive Performance**: Automatic performance optimization based on device capabilities
- **XR Support**: Full support for extended reality experiences
- **Cross-Platform**: Compatible with Android and iOS

## 🛠️ Technologies Used

- **Unity Engine**: Main development engine
- **Google Cardboard SDK**: For VR functionality
- **C#**: Programming language
- **Android Gradle**: For Android builds
- **XR Plugin Management**: Extended reality plugin management

## 📋 System Requirements

### For Development
- Unity 2022.3 or higher
- Visual Studio or Visual Studio Code
- Android SDK (for Android builds)
- Git

### For End Users
- Android/iOS mobile device
- Google Cardboard or compatible VR viewer
- Android 7.0+ or iOS 11.0+ operating system

## 🚀 Installation and Setup

### Clone the Repository
```bash
git clone https://github.com/alanjosue1998/cuicocha-vr.git
cd cuicocha-vr
```

### Configure Unity
1. Open Unity Hub
2. Click "Add" and select the project folder
3. Make sure you have Unity 2022.3 or higher installed
4. Open the project in Unity

### Configure for Android
1. Go to `File > Build Settings`
2. Select "Android" as platform
3. Click "Switch Platform"
4. Configure Player Settings according to your needs

## 🎮 Usage

1. Build the project for your mobile device
2. Install the application on your device
3. Place the device in your Google Cardboard viewer
4. Enjoy the VR experience!

## 📁 Project Structure

```
cuicocha-vr/
├── Assets/
│   ├── Adaptive Performance/     # Adaptive performance configurations
│   ├── Editor/                   # Editor scripts
│   ├── Plugins/
│   │   └── Android/             # Android-specific configurations
│   ├── Samples/                 # Sample content
│   ├── Scenes/                  # Game scenes
│   ├── TutorialInfo/            # Tutorial information
│   └── XR/                      # Extended reality configurations
├── ProjectSettings/             # Project configurations
├── Packages/                    # Unity packages
└── README.md                    # This file
```

## 🔧 Advanced Configuration

### Gradle Configuration (Android)
The project includes custom Gradle configurations in `Assets/Plugins/Android/mainTemplate.gradle` to optimize Android builds.

### XR Configuration
Extended reality configurations are located in `Assets/XR/` and allow you to customize the VR experience according to your needs.

## 🤝 Contributing

1. Fork the project
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License. See the `LICENSE` file for more details.

## 📞 Contact

If you have questions or suggestions about this project, feel free to contact me:

- **GitHub**: [alanjosue1998](https://github.com/alanjosue1998)
- **Email**: alanjosue1998@example.com

## 🙏 Acknowledgments

- Google Cardboard for the VR SDK
- Unity Technologies for the development engine
- VR developer community

---

⭐ Don't forget to give the project a star if you like it!
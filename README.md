# Cuicocha VR Experience

Un proyecto de realidad virtual desarrollado en Unity utilizando Google Cardboard para crear una experiencia inmersiva.

## 📱 Descripción

Este proyecto implementa una experiencia de realidad virtual utilizando Google Cardboard, permitiendo a los usuarios explorar contenido 3D a través de dispositivos móviles con soporte VR.

## 🚀 Características

- **Compatibilidad con Google Cardboard**: Optimizado para dispositivos móviles con soporte VR
- **Experiencia inmersiva**: Contenido 3D interactivo
- **Adaptive Performance**: Optimización automática del rendimiento según el dispositivo
- **XR Support**: Soporte completo para experiencias de realidad extendida
- **Multiplataforma**: Compatible con Android e iOS

## 🛠️ Tecnologías Utilizadas

- **Unity Engine**: Motor de desarrollo principal
- **Google Cardboard SDK**: Para funcionalidades VR
- **C#**: Lenguaje de programación
- **Android Gradle**: Para builds de Android
- **XR Plugin Management**: Gestión de plugins de realidad extendida

## 📋 Requisitos del Sistema

### Para Desarrollo
- Unity 2022.3 o superior
- Visual Studio o Visual Studio Code
- Android SDK (para builds de Android)
- Git

### Para Usuarios Finales
- Dispositivo móvil Android/iOS
- Google Cardboard o visor VR compatible
- Sistema operativo Android 7.0+ o iOS 11.0+

## 🚀 Instalación y Configuración

### Clonar el Repositorio
```bash
git clone https://github.com/tu-usuario/cuicocha.git
cd cuicocha
```

### Configurar Unity
1. Abre Unity Hub
2. Haz clic en "Add" y selecciona la carpeta del proyecto
3. Asegúrate de tener Unity 2022.3 o superior instalado
4. Abre el proyecto en Unity

### Configurar para Android
1. Ve a `File > Build Settings`
2. Selecciona "Android" como plataforma
3. Haz clic en "Switch Platform"
4. Configura las opciones de Player Settings según tus necesidades

## 🎮 Uso

1. Compila el proyecto para tu dispositivo móvil
2. Instala la aplicación en tu dispositivo
3. Coloca el dispositivo en tu visor Google Cardboard
4. ¡Disfruta de la experiencia VR!

## 📁 Estructura del Proyecto

```
cuicocha/
├── Assets/
│   ├── Adaptive Performance/     # Configuraciones de rendimiento adaptativo
│   ├── Editor/                   # Scripts del editor
│   ├── Plugins/
│   │   └── Android/             # Configuraciones específicas de Android
│   ├── Samples/                 # Contenido de ejemplo
│   ├── Scenes/                  # Escenas del juego
│   ├── TutorialInfo/            # Información de tutoriales
│   └── XR/                      # Configuraciones de realidad extendida
├── ProjectSettings/             # Configuraciones del proyecto
├── Packages/                    # Paquetes de Unity
└── README.md                    # Este archivo
```

## 🔧 Configuración Avanzada

### Configuración de Gradle (Android)
El proyecto incluye configuraciones personalizadas de Gradle en `Assets/Plugins/Android/mainTemplate.gradle` para optimizar el build de Android.

### Configuración XR
Las configuraciones de realidad extendida se encuentran en `Assets/XR/` y permiten personalizar la experiencia VR según tus necesidades.

## 🤝 Contribuir

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 📞 Contacto

Si tienes preguntas o sugerencias sobre este proyecto, no dudes en contactarme:

- **GitHub**: [tu-usuario](https://github.com/tu-usuario)
- **Email**: tu-email@ejemplo.com

## 🙏 Agradecimientos

- Google Cardboard por el SDK de VR
- Unity Technologies por el motor de desarrollo
- Comunidad de desarrolladores VR

---

⭐ ¡No olvides darle una estrella al proyecto si te gusta!

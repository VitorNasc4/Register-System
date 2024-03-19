# Register-System

API for a user registration and authentication system. Allows registration, authentication, and updating of user data using cache to improve the performance. It is integrated with a notification system that sends a welcome email to new users.


## 🛠 Tecnologies used

* Dotnet 8.0
* Postgres
* Redis
* RabbitMQ
* Dokcer


<h4 align="center"> 
 🚧  Project Status: Finished  🚧
</h4>

## Demonstration

![image](https://github.com/VitorNasc4/Register-System/assets/101666833/43b5deda-51cb-49bb-b4f5-1bd0b97e7b78)


## How to use

1) Running Tests
   At the root of the project:
   - cd .\GenericAPI\src\ProjectName.Test\
   - dotnet test

2) Running application
    - Starting Database and RabbitMQ with docker:
      After start docker, at the root of the project:
      - docker compose up -d
    
    - Starting API (Database and RabbitMQ must be healthy)
      At the root of the project:
      - cd .\GenericAPI\src\ProjectName.API\
      - dotnet run
     
    - Starting Notification Service (RabbitMQ must be healthy)
      At the root of the project:
      - cd .\GenericNotificationService\NotificationService\
      - dotnet run


📫 You cand find me through those links below...

## 🔗 Links
[![linkedin](https://img.shields.io/badge/linkedin-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/vitor-marciano/)
[![twitter](https://img.shields.io/badge/twitter-1DA1F2?style=for-the-badge&logo=twitter&logoColor=white)](https://twitter.com/marciano_vitor)

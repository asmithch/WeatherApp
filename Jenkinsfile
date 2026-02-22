pipeline {

    agent any

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore .NET Packages') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build Backend') {
            steps {
                bat 'dotnet build WeatherApp.sln --configuration Release'
            }
        }

        stage('SonarQube Analysis') {
            steps {
                bat 'dotnet tool install --global dotnet-sonarscanner --ignore-failed-sources || echo already installed'
                bat 'dotnet sonarscanner begin /k:"WeatherApp" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="sqa_a07e82f1bed11db5cb8b844b4813ec24818cc0c8"'
                bat 'dotnet build WeatherApp.sln'
                bat 'dotnet sonarscanner end /d:sonar.login="YOUR_SONAR_TOKEN"'
            }
        }

        stage('Build Angular UI') {
            steps {
                dir('WeatherAppUI') {
                    bat 'npm install'
                    bat 'npm run build'
                }
            }
        }

        stage('Publish Backend') {
            steps {
                bat 'dotnet publish AuthenticationService.API/AuthenticationService.API.csproj -c Release -o ./publish/auth'
                bat 'dotnet publish WeatherService.API/WeatherService.API.csproj -c Release -o ./publish/weather'
                bat 'dotnet publish LoggingService.API/LoggingService.API.csproj -c Release -o ./publish/logging'
            }
        }

    }

    post {
        success {
            echo "Build Successful ✅"
        }
        failure {
            echo "Build Failed ❌"
        }
    }

}
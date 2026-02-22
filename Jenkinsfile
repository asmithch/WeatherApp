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
                bat 'dotnet build WeatherApp.sln'
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
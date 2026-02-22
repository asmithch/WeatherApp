pipeline {

    agent any

    tools {
        nodejs "NodeJS"
    }

    stages {

        /* ✅ Clone Repository */
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        /* ✅ Restore .NET Dependencies */
        stage('Restore Backend') {
            steps {
                bat 'dotnet restore'
            }
        }

        /* ✅ Build Backend Services */
        stage('Build Backend') {
            steps {
                bat 'dotnet build WeatherApp.sln'
            }
        }

        /* ✅ Build Angular Frontend */
        stage('Build Angular UI') {
            steps {
                dir('WeatherAppUI') {
                    bat 'npm install'
                    bat 'npm run build'
                }
            }
        }

        /* ✅ Testing Phase */
        stage('Testing') {
            steps {
                echo "Running Tests"
            }
        }

    }

    post {

        success {
            echo 'Pipeline Execution Successful ✅'
        }

        failure {
            echo 'Pipeline Failed ❌'
        }

    }
}
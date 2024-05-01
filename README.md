Chemistry Cafe API
==============

API for the Chemistry Cafe web application found here https://github.com/NCAR/chemistrycafe


# Getting Started

## Installing the API locally
To build and install Chemistry Cafe locally, you must have Visual Studio 2022.

Simply clone the project then open the .sln file in Visual Studio.

To change the locaiton of the web application, navigate to the Progran.cs folder then change the IP address on line 22.

To change the database location, navigate to the appsetting.json and change the Default Connection String on line 10.

# Database
The database that the API is meant to call was made in MySQL. A script to create all the tables can be found in the file chemistry-cafe-database.sql.

# Using the MICM API

When ran in development mode the API launches a Swagger page with all the routes and the various inputes and outputs.

Most routes take in JSON as outputs and give responses as JSON.


# License

- [Apache 2.0](/LICENSE)

Copyright (C) 2018-2024 National Center for Atmospheric Research

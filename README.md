# Fullstack Project - E-commerce platform

![Emoji](https://img.shields.io/badge/STILL_IN_PROGRESS-YES-red)
![Emoji](https://img.shields.io/badge/author-MI-blue)

![TypeScript](https://img.shields.io/badge/TypeScript-v.4-green)
![SASS](https://img.shields.io/badge/SASS-v.4-hotpink)
![React](https://img.shields.io/badge/React-v.18-blue)
![Redux toolkit](https://img.shields.io/badge/Redux-v.1.9-brown)
![.NET Core](https://img.shields.io/badge/.NET%20Core-v.7-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-v.7-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-v.14-darkblue)

## Fullstack Assignment using react on frontend and .NET Core on backend

The purpose of this project was to build a fullstack application using react on frontend and .NET EF Core on backend with PostreSQL database. In the backend clean architecture had been applied and a function based structure in the frontend. The project is a webshop where you can buy different products. The project is built with the following technologies:

- Frontend: SASS, TypeScript, React, Redux Toolkit, Material UI
- Backend: ASP .NET Core, Entity Framework Core, PostgreSQL

## Table of contents

- [Documetation](#documetation)
- [Deployment](#deployment)
- [Functionalities](#functionalities)
  - [Missing functionalities](#missing-functionalities)
- [Issues](#issues-to-fix)
- [Getting started](#getting-started)
- [Project structure](#project-structure)

### Documetation

- ERR, requirements Analysis and Architecture plan can be found from the project desing board from: **[here](https://miro.com/app/board/uXjVMxMsI9Y=/?share_link_id=849947316130)**
- Detailed API endpoint documentation can be found from Swagger from this page: **[here](https://mi-eshop.azurewebsites.net/swagger/index.html)**
- Database lives in ElephantSQL and the backend is deployed to Azure.
- Frontend is deployed to cloudflare and can be found from: **[here](https://532962c3.frontend-1rn.pages.dev/)**

### Deployment

Checkout the project from here: **[here](https://532962c3.frontend-1rn.pages.dev/)**

### Functionalities

- Backend Api endpoint functionalities are listed here: **[here](https://mi-eshop.azurewebsites.net/swagger/index.html)**

- Frontend:

  - User:
    - Can create account and login. They can browse products, see their own profile details and place an order via cart.
  - Admin:
    - Can login, see all the users and products. They can create, update and delete users and products. They can also see all the orders.
    - Only admin can create admin user.
    - To test admin functionalities use:
      - email: admin@mail.com
      - password: admin123

#### Missing functionalities

- Design for the order list page is not done and the mapping with unique key is not done.
- Normal user is not able to update their own details, delete the account or see their own products.
- No payment feature done.
- Not all user alerts are applied, for example when product is updated, it does not give a notice if it was updated or not.
- Indications that app is loading when fetching data from API has not been done.
- Validation for all the forms has not been done.

### Issues to fix

- General:

  - Deployed application is extremely slow and crashes a lot (needs restarting many of the occations) probably due to the backend issues (ans Azure free tier)?
  - Testing in the backend has been very little done and not at all in the frontend.
  - Refactoring the code and cleaning up the code (For example: MUI code is not seperated in a seperate file, but in the same file as the component, lots of unused codes etc.).
  - Optimizing the code and performance.

- Frontend:

  - Filters and search does not work properly together: when filtering is applied it wont keep the filtered order. Trying these many times also crashes the application.
  - Not able to add actual image file when creating new user or product, only URL is possible to add. Not able to add severeal image urls for product.
  - When refresshing the page in the pages taht are restricted via private route, it will always redirect to login page even the user is already logged in as an admin.

- Backend:
  - Updating API:s are not functioning properly : it will update all the fields even if the field is not updated. In the order update it doesn't update the order status, but the updated date is updated.
  - Performance and obtimization has not been thought about.

### Getting started

1.  To clone repository use: git clone

- Frontend:

  1.  Navigate to the frontend folder (cd frontend)
  2.  To install the project run: npm install
  3.  Refer to package.json for scripts
  4.  To run application in development mode run: npm start
  5.  To run tests run: npm test
  6.  To build application for production: npm run build

- Backend:
  1.  Navigate to the backend folder (cd backend)
  2.  Prerequisites
      - .NET SDK V 7
      - Visual Studio, Visual Studio Code or somtehing like that
      - PostreSQL Server or another supported database
  3.  Configure the connection string in the appsettings.json file:
      ```
      "ConnectionStrings": {
      "DefaultConnection": "Server=<server_name>;Database=<database_name>;User Id=<username>;Password=<password>;"
      }
      ```
  4.  Apply migrations to the database: dotnet ef database update
  5.  Run the backend locally at: http://localhost:port run: dotnet watch

### Project structure

- Backend folder elements:

```

├───WebApi.Business
│   └───src
│       ├───Dtos
│       ├───Services
│       │   ├───ServiceImplementations
│       │   └───ServiceInterfaces
│       └───Shared
├───WebApi.Controller
│   └───src
│       └───Controllers
├───WebApi.Domain
│   └───src
│       ├───Entities
│       ├───RepoInterfaces
│       └───Shared
├───WebApi.Infrastructure
│   └───src
│       ├───AuthorizationRequirement
│       ├───Configuration
│       ├───Database
│       └───RepoImplimentations
└───WebApi.Testing
    └───src
```

- Frontend folder elements:

```
├───public
└───src
    ├───api
    ├───components
    │   └───admin_components
    ├───hooks
    ├───img
    ├───pages
    ├───redux
    │   └───reducers
    ├───routes
    ├───styles
    ├───types
    ├───utils
    └───validation
```

# Prueba Tecnica Api

This is a .Net Core v3.1 Web Api. This API implements docker.

<br>

# Architecture

This project it's build under onion architecture this solution has these layers:

##### **1. Domain**:

- **Entities**: Business models
- **Interfaces**: Business contracts
- **Services**: Business logic implementation

##### **2. Infrastructure**: External Agents

- **Data**: Implements Cache Redis Repository
- **IoC**: Implements Dependency Injection

#### **3. Application**: Layer for UI access

- **Contracts**: Contains the necessary DTO's
- **Interfaces**: UI Contracts
- **Services**: UI Services (Implements UI Contracts)

#### **4. WebApi**: API for the client access

- **Controllers**: Endpoints for the application access
- **Middleware**: Implements Global Event Handler and Logger.

#### **5. Test**: Layer test

- **UnitTest**: Test for bussines logic (services).
- **IntegrationTest**: Test for verify correct flow between application layers (controllers).

<br>

# Instructions

#### **1.** Clone the Repo

#### **2.** If required change port bindings in docker-compose.yml

#### **3.** Execute from the root folder `docker-compose build`

#### **4.** Start the everything up `docker-compose up`

#### **5.** Open browser in `http://localhost:{DefaultPort}/swagger`. **DefaultPort: 6001**

<br>

# Test Data
## Positive Case
### Body
{
    "satellites": [<br>
        {<br>
        "name": "kenobi",<br>
        "distance": 500,<br>
        "message": ["este", "", "", "mensaje", ""]<br>
        },<br>
        {<br>
        "name": "skywalker",<br>
        "distance": 500,<br>
        "message": ["", "es", "", "", "secreto"]<br>
        },<br>
        {<br>
        "name": "sato",<br>
        "distance": 1300,<br>
        "message": ["este", "", "un", "", ""]<br>
        }<br>
    ]<br>
}<br>
### coordinates into appSettings
- Kenobi: [500, 400]
- Skywalker: [400, -300]
- Sato: [-400, 1300]

<br>

## Negative Case
### Body
{
    "satellites": [<br>
        {<br>
        "name": "kenobi",<br>
        "distance": 84,<br>
        "message": ["este", "", "", "mensaje", ""]<br>
        },<br>
        {<br>
        "name": "skywalker",<br>
        "distance": 114,<br>
        "message": ["", "es", "", "", "secreto"]<br>
        },<br>
        {<br>
        "name": "sato",<br>
        "distance": 120,<br>
        "message": ["este", "", "un", "", ""]<br>
        }<br>
    ]<br>
}<br>
### coordinates into appSettings
- Kenobi: [-19.6685, -69.1942]
- Skywalker: [-20.2705, -70.1311]
- Sato: [-20.5656, -70.1807]


## Body Not message
{
    "satellites": [<br>
        {<br>
        "name": "kenobi",<br>
        "distance": 500,<br>
        "message": ["", "", "", "", ""]<br>
        },<br>
        {<br>
        "name": "skywalker",<br>
        "distance": 500,<br>
        "message": ["", "", "", "", ""]<br>
        },<br>
        {<br>
        "name": "sato",<br>
        "distance": 1300,<br>
        "message": ["", "", "", "", ""]<br>
        }<br>
    ]<br>
}<br>

## Body Message Incomplete
{
    "satellites": [<br>
        {<br>
        "name": "kenobi",<br>
        "distance": 500,<br>
        "message": ["este", "", "", "", ""]<br>
        },<br>
        {<br>
        "name": "skywalker",<br>
        "distance": 500,<br>
        "message": ["", "es", "", "", ""]<br>
        },<br>
        {<br>
        "name": "sato",<br>
        "distance": 1300,<br>
        "message": ["este", "", "un", "", ""]<br>
        }<br>
    ]<br>
}<br>


# Author

- #### **Camilo Vahos** - camivalo01@gmail.com

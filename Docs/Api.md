# BuberDinner API

-   [Auth](#auth)
    -   [Register](#register)
    -   [Login](#login)

### Register

```js
POST {{host}}/auth/register
```

#### Register Request

```json
{
    "firstName": "David",
    "lastName": "Dev",
    "email": "david@example.com",
    "password": "password123"
}
```

#### Register Response

```json
200 OK
```

```json
{
    "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
    "firstName": "David",
    "lastName": "Dev",
    "email": "david@example.com",
    "token": "eyJhb..hbbQ"
}
```

### Login

```js
POST {{host}}/auth/login
```

#### Login Request

```json
{
    "email": "david@example.com",
    "password": "password123"
}
```

#### Login Response

```json
200 OK
```

```json
{
    "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
    "firstName": "David",
    "lastName": "Dev",
    "email": "david@example.com",
    "token": "eyJhb..hbbQ"
}
```

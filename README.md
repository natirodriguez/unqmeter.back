# UNQMeter - BackEnd
<h4>Decisiones de desarrollo:</h4> 

* c# v.10
* .NET Core 6
* Angular 10
* Entity Framework
* Sql Server
* Bootstrap
* angularx-social-login
* Swagger
* Test: NUnit y Mock
* Docker

Hemos decidido trabajar con lo mencionado anteriormente, ya que ambos trabajamos actualmente en ello y nos sentimos comodos utilizando estas herramientas. Evitando de esta forma, el tener que aprender o familiarizarse de nuevo con un lenguaje o framework web. 


<h4>Métodos HTTP: </h4>

* <b>POST:</b> /api/Login/ExternalLogin <br>
Método utilizado para el login con google.

* <b>GET:</b> /api/Presentation/GetMisPresentaciones/{email} <br>
Método utilizado para obtener todas las presentaciones realizadas por la persona con el email pasado por parametro.

* <b>GET:</b> /api/Presentation/GetPresentacion/{id} <br>
Método utilizado para obtener la presentación correspondiente al id pasado por parametro.

* <b>GET:</b> /api/GetSlydesByIdPresentation/{presentationId} <br>
Método utilizado para obtener las slydes correspondientes a una determinada presentacion, pasando por parametro su Id.

* <b>GET:</b> /api/Presentation/GetTipoPreguntas <br>
Método utilizado para traer todos los tipos de preguntas del sistema.

* <b>GET:</b> /api/Presentation/EstaVencidaLaPresentacion/{presentationId} <br>
Método utilizado para saber si una presentación se encuentra vencida. Se encuentra vencida en caso de que tenga fecha de vencimiento y la misma, sea mayor a la fecha actual.

* <b>GET:</b> /api/Presentation/GetSlydesAnswersByIdPresentation/{presentationId} <br>
Método utilizado para obtener las slydes que tuvieron una respuesta por parte del usuario.

* <b>GET:</b> /api/RespuestaParticipante/GetSlydesSinRespuestas/{idPresentacion} <br>
Método utilizado para saber las slydes que no tuvieron respuesta por parte del usuario.

* <b>POST:</b> /api/Presentation/PostNuevaPresentacion <br>
Método utilizado para generar una nueva presentación.

* <b>POST:</b> /api/Presentation/GetClonarPresentacion/ <br>
Método utilizado para clonar la presentación que tenga determinado Id. Clona la presentación, la slyde y las opciones slyde correspondientes.

* <b>POST:</b> /api/Presentation/PostCompartirPresentacion <br>
Método utilizado para setear la fecha de inicio y fin de la presentación, según lo cargado en los parametros tiempo de vida.

* <b>POST:</b> /api/Presentation/SaveSlyde <br>
Método utilizado para guardar una slyde.

* <b>POST:</b> /api/RespuestaParticipante/SaveRespuesta <br>
Método utilizado para guardar la respuesta del usuario.

* <b>DELETE:</b> /api/Presentation/DeleteSlyde/{slydeId} <br>
Método utilizado para eliminar la slyde correspondiente al id pasado por parametro.

* <b>DELETE:</b> /api/Presentation/DeletePresentacion/{presentacionId} <br>
Método utilizado para eliminar la presentación correspondiente al id pasado por parametro.

* <b>DELETE:</b> /api/Presentation/DeleteOptionSlyde/{optionSlydeId} <br>
Método utilizado para eliminar la opcion de slyde correspondiente al id pasado por parametro.

<br/>

## Instalación ambiente de desarollo

### Prerequisitos
Necesitamos tener instalado las siguientes herramientas:

* [Visual Studio 2019 o 2022](https://visualstudio.microsoft.com/downloads/)
* [.Net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [Sql server 2017 o posterior](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) 

<br/>

### Instalación
Sigue estos pasos para instalar el ambiente de desarrollo:

1. Modifique la propiedad ConnectionStrings en el archivo ```appsettings.json``` para que apunte a su instancia local de SQL Server. 

2. Clonar el repositorio [UnqMeterBack](https://github.com/natirodriguez/unqmeter.back)
3. En el directorio raíz restaure los paquetes ejecutando:
```csharp
dotnet restore
```
4. Compile la solución ejecutando:
```csharp
dotnet build
```
5. Luego dentro del directorio inicie el backend ejecutando:
```csharp
dotnet run
```

6. Inicie https://localhost:7054/swagger en su navegador para ver la interfaz de Swagger.

<br/>

## Instalación ambiente de desarollo con Docker

### Prerequisitos
Necesitamos tener instalado las siguientes herramientas:


* [Docker](https://docs.docker.com)
* [Docker Compose](https://docs.docker.com/compose/install/)

Sigue estos pasos para instalar el ambiente de desarrollo con Docker:

1. Clonar el repositorio [UnqMeterBack](https://github.com/natirodriguez/unqmeter.back)

2. Abrir una consola en el directorio raiz del proyecto y ejecutar el siguiente comando:
```sh
docker compose up

Para ejecutar el servicio en segundo plano puede utilizar el flag -d (para el modo "detached"):

docker compose up -d

Y con el comando "docker compose ps" puede ver lo que se esta ejecutando actualmente
```
3. Iniciar https://localhost:7054/swagger en su navegador para ver la interfaz de Swagger.

4. Para detener los servicios utilice la siguiente instrucción:
```sh
docker compose stop
```

<br/>

<h4>Diagrama de clases: </h4>

<br/>

![Untitled Diagram (3)](https://user-images.githubusercontent.com/1548366/193464258-ee132e87-a796-4749-8006-175292dc06f0.jpg)



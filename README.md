# Programación III - 2025

## Luis Carlos Lima Pérez
## Carnet: 0907-23-20758

# Implementaciones Manuales de Pilas(Stack) y Colas(Queue)

## Descripción

Este proyecto incluye dos implementaciones:

1. **Verificador de Paréntesis Balanceados**: Utiliza una pila (Stack) para verificar si los paréntesis en una expresión aritmética están balanceados.

2. **Sistema de Gestión de Colas**: Implementa el patrón de inyección de dependencias para trabajar con diferentes proveedores de servicios de cola.

## Estructura del Proyecto

- `Program.cs`: Contiene el menú principal y la lógica de navegación.
- `ParenthesisChecker.cs`: Implementa la lógica para verificar paréntesis balanceados.
- `IQueueService.cs`: Define la interfaz para los servicios de cola.
- `RabbitMQService.cs`: Implementación simulada del servicio RabbitMQ.
- `AmazonSQSService.cs`: Implementación simulada del servicio Amazon SQS.
- `AzureQueueService.cs`: Implementación simulada del servicio Azure Queue.
- `QueueManager.cs`: Clase que gestiona las operaciones con las colas.

## Ejecución

1. Compile el proyecto con `dotnet build`
2. Ejecute la aplicación con `dotnet run`

## Menú Principal

Este menú es lo primero que va a mostrar el programa al ejecutarse, este servirá para que pueda escoger cuál de los dos enunciados de la tarea quiere ejecutar.

### Elección de Verfificador de Paréntesis

Para elegir el verificador de paréntesis que quiere realizar (crear una expresión propia o escoger la que esta por defecto), seleccione cualquiera de las dos opciones del menú de "Verificador de Balance de Paréntesis".

### Cambio de Proveedor de Cola

Para cambiar el proveedor de cola, seleccione la opción correspondiente en el menú de "Gestión de Colas".
El sistema está diseñado siguiendo el principio de inversión de dependencias, por lo que añadir nuevos proveedores es tan sencillo como crear una nueva clase que implemente la interfaz `IQueueService`.

# Link hacia el video de prueba
https://drive.google.com/drive/folders/1BC4mV7llisbgHM7V7I_qOsZQlzQPE7B1?usp=sharing

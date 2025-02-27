
namespace PilasYColas;

    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("====== MENÚ PRINCIPAL ======");
                Console.WriteLine("1. Verificar balance de paréntesis");
                Console.WriteLine("2. Gestión de colas (Queue Service)");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccione una opción: ");
                
                string? input = Console.ReadLine();
                
                switch (input)
                {
                    case "1":
                        RunParenthesisBalanceChecker();
                        break;
                    case "2":
                        RunQueueManager();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        
        static void RunParenthesisBalanceChecker()
        {
            Console.Clear();
            Console.WriteLine("==== Verificador de Balance de Paréntesis ====\n");
            Console.WriteLine("1. Ingresar expresión personalizada");
            Console.WriteLine("2. Usar expresión por defecto: (((x + 1) - 3) * (x * x + 2) - 4) + (x - 1)");
            Console.Write("\nSeleccione una opción: ");
        
            string? option = Console.ReadLine();
            string expresion;
        
            if (option == "1")
            {
                Console.Write("\nIngrese una expresión para verificar: ");
                expresion = Console.ReadLine() ?? "";
                
                if (string.IsNullOrWhiteSpace(expresion))
                {
                    Console.WriteLine("\nLa expresión no puede estar vacía. Usando expresión por defecto.");
                    expresion = "(((x + 1) - 3) * (x * x + 2) - 4) + (x - 1)";
                }
            }
            else
            {
                expresion = "(((x + 1) - 3) * (x * x + 2) - 4) + (x - 1)";
            }
        
            Console.WriteLine($"\nExpresión: {expresion}");
            Console.WriteLine($"¿Está balanceada? {ParenthesisChecker.EstaBalanceada(expresion)}");
        
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }
        
        static void RunQueueManager()
        {
            Console.Clear();
            Console.WriteLine("==== Gestión de Colas (Queue Service) ====\n");
            Console.WriteLine("Seleccione el proveedor de cola:");
            Console.WriteLine("1. RabbitMQ");
            Console.WriteLine("2. Amazon SQS");
            Console.WriteLine("3. Azure Queue Storage");
            Console.Write("\nSeleccione una opción: ");
            
            IQueueService queueService = null;
            bool validSelection = false;
            
            while (!validSelection)
            {
                string? input = Console.ReadLine();
                
                switch (input)
                {
                    case "1":
                        queueService = new RabbitMQService();
                        validSelection = true;
                        break;
                    case "2":
                        queueService = new AmazonSQSService();
                        validSelection = true;
                        break;
                    case "3":
                        queueService = new AzureQueueService();
                        validSelection = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor seleccione 1, 2 o 3:");
                        break;
                }
            }
            
            QueueManager queueManager = new QueueManager(queueService);
            
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("==== Operaciones de Cola ====");
                Console.WriteLine($"Proveedor actual: {queueService.GetType().Name}");
                Console.WriteLine($"Elementos en cola: {queueManager.GetQueueCount()}");
                Console.WriteLine("\n1. Agregar mensaje a la cola");
                Console.WriteLine("2. Obtener mensaje de la cola");
                Console.WriteLine("3. Verificar si la cola está vacía");
                Console.WriteLine("4. Mostrar contenido de la cola");
                Console.WriteLine("0. Volver al menú principal");
                Console.Write("\nSeleccione una opción: ");
                
                string? input = Console.ReadLine();
                
                switch (input)
                {
                    case "1":
                        Console.Write("\nIngrese el mensaje a enviar: ");
                        string? message = Console.ReadLine();
                        queueManager.SendMessage(message);
                        break;
                    case "2":
                        string receivedMessage = queueManager.ReceiveMessage();
                        Console.WriteLine($"\nMensaje recibido: {receivedMessage}");
                        break;
                    case "3":
                        bool isEmpty = queueManager.IsQueueEmpty();
                        Console.WriteLine($"\nLa cola está vacía: {isEmpty}");
                        break;
                    case "4":
                        queueManager.PrintQueueContents();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                
                if (!back)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }


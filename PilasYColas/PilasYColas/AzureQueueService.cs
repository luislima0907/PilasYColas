using System.Text;

namespace PilasYColas;

    // Implementación para Azure Queue Storage usando nodos enlazados
    public class AzureQueueService : IQueueService
    {
        // Clase interna para los nodos de la cola enlazada
        private class QueueNode
        {
            public string Message { get; set; }
            public QueueNode Next { get; set; }
            
            public QueueNode(string message)
            {
                Message = message;
                Next = null;
            }
        }
        
        private QueueNode _head; // Primer nodo (para desencolado)
        private QueueNode _tail; // Último nodo (para encolado)
        private int _count; // Contador de elementos
        
        public AzureQueueService()
        {
            _head = null;
            _tail = null;
            _count = 0;
            Console.WriteLine("Azure Queue Storage: Servicio inicializado");
        }
        
        public void Enqueue(string message)
        {
            Console.WriteLine("\n=== INICIO OPERACIÓN ENQUEUE AZURE ===");
            Console.WriteLine($"Azure Queue Storage: Estado inicial - Count: {_count}, Head: {(_head == null ? "null" : "set")}, Tail: {(_tail == null ? "null" : "set")}");
            
            // Crear un nuevo nodo con el mensaje
            QueueNode newNode = new QueueNode(message);
            Console.WriteLine($"Azure Queue Storage: Nuevo nodo creado con mensaje: \"{message}\"");
            
            // Si la cola está vacía, el nuevo nodo será tanto head como tail
            if (_head == null)
            {
                Console.WriteLine("Azure Queue Storage: Cola vacía, el nuevo nodo será head y tail");
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                Console.WriteLine("Azure Queue Storage: Agregando nuevo nodo al final de la cola");
                // Agregar el nuevo nodo al final y actualizar tail
                _tail.Next = newNode;
                _tail = newNode;
            }
            
            // Incrementar el contador
            _count++;
            Console.WriteLine($"Azure Queue Storage: Count incrementado a {_count}");
            
            DisplayQueueState();
            Console.WriteLine("=== FIN OPERACIÓN ENQUEUE AZURE ===\n");
        }
        
        public string Dequeue()
        {
            Console.WriteLine("\n=== INICIO OPERACIÓN DEQUEUE AZURE ===");
            Console.WriteLine($"Azure Queue Storage: Estado inicial - Count: {_count}, Head: {(_head == null ? "null" : "set")}, Tail: {(_tail == null ? "null" : "set")}");
            
            if (_head == null)
            {
                Console.WriteLine("Azure Queue Storage: La cola está vacía");
                Console.WriteLine("=== FIN OPERACIÓN DEQUEUE AZURE ===\n");
                return "La cola está vacía";
            }
            
            // Obtener el mensaje del nodo head
            string message = _head.Message;
            Console.WriteLine($"Azure Queue Storage: Mensaje encontrado en head: \"{message}\"");
            
            // Actualizar head para que apunte al siguiente nodo
            _head = _head.Next;
            Console.WriteLine($"Azure Queue Storage: Head actualizado al siguiente nodo: {(_head == null ? "null" : "set")}");
            
            // Si head se convirtió en null, también tail debe ser null
            if (_head == null)
            {
                Console.WriteLine("Azure Queue Storage: Cola ahora vacía, actualizando tail a null");
                _tail = null;
            }
            
            // Decrementar el contador
            _count--;
            Console.WriteLine($"Azure Queue Storage: Count decrementado a {_count}");
            
            DisplayQueueState();
            Console.WriteLine($"Azure Queue Storage: Mensaje desencolado: \"{message}\"");
            Console.WriteLine("=== FIN OPERACIÓN DEQUEUE AZURE ===\n");
            
            return message;
        }
        
        public bool IsEmpty()
        {
            bool isEmpty = _head == null;
            Console.WriteLine($"Azure Queue Storage: Verificando si está vacía - Head: {(_head == null ? "null" : "set")}, ¿Está vacía?: {isEmpty}");
            return isEmpty;
        }
        
        public int Count()
        {
            Console.WriteLine($"Azure Queue Storage: Contando elementos - Count: {_count}");
            return _count;
        }
        
        private void DisplayQueueState()
        {
            Console.WriteLine("Azure Queue Storage: Estado actual de la cola:");
            StringBuilder queueVisual = new StringBuilder();
            queueVisual.Append("HEAD -> ");
            
            QueueNode current = _head;
            int nodeCount = 0;
            int displayLimit = 10; // Mostrar máximo 10 elementos
            
            while (current != null && nodeCount < displayLimit)
            {
                queueVisual.Append($"[{current.Message}] -> ");
                current = current.Next;
                nodeCount++;
            }
            
            if (current != null)
            {
                queueVisual.Append("... -> ");
            }
            
            queueVisual.Append("TAIL");
            Console.WriteLine(queueVisual.ToString());
            Console.WriteLine($"Elementos totales: {_count}");
        }
        
        public void PrintQueue()
        {
            Console.WriteLine("\n=== CONTENIDO DE COLA AZURE ===");
            if (_head == null)
            {
                Console.WriteLine("Azure Queue Storage: La cola está vacía");
            }
            else
            {
                DisplayQueueState();
            }
            Console.WriteLine("=== FIN CONTENIDO COLA AZURE ===\n");
        }
    }
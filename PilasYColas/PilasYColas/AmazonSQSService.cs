using System.Text;

namespace PilasYColas;

    // Implementación para Amazon SQS usando arrays internamente
    public class AmazonSQSService : IQueueService
    {
        // Implementamos la cola usando un array circular
        private string[] _queueArray;
        private int _head; // Índice del primer elemento
        private int _tail; // Índice donde se insertará el próximo elemento
        private int _size; // Cantidad actual de elementos
        private int _capacity; // Tamaño del array
        
        public AmazonSQSService(int initialCapacity = 10)
        {
            _capacity = initialCapacity;
            _queueArray = new string[_capacity];
            _head = 0;
            _tail = 0;
            _size = 0;
            Console.WriteLine($"Amazon SQS: Servicio inicializado con capacidad {_capacity}");
        }
        
        public void Enqueue(string message)
        {
            Console.WriteLine("\n=== INICIO OPERACIÓN ENQUEUE SQS ===");
            Console.WriteLine($"Amazon SQS: Estado inicial - Head: {_head}, Tail: {_tail}, Size: {_size}, Capacity: {_capacity}");
            
            // Verificar si necesitamos aumentar la capacidad
            if (_size == _capacity)
            {
                Console.WriteLine($"Amazon SQS: Capacidad máxima alcanzada, redimensionando array");
                // Duplicar el tamaño del array
                Resize(_capacity * 2);
            }
            
            // Insertar el mensaje en la posición tail
            _queueArray[_tail] = message;
            Console.WriteLine($"Amazon SQS: Mensaje insertado en posición {_tail}: \"{message}\"");
            
            // Actualizar tail circularmente
            _tail = (_tail + 1) % _capacity;
            Console.WriteLine($"Amazon SQS: Tail actualizado a {_tail}");
            
            // Incrementar el tamaño
            _size++;
            Console.WriteLine($"Amazon SQS: Size incrementado a {_size}");
            
            DisplayQueueState();
            Console.WriteLine("=== FIN OPERACIÓN ENQUEUE SQS ===\n");
        }
        
        public string Dequeue()
        {
            Console.WriteLine("\n=== INICIO OPERACIÓN DEQUEUE SQS ===");
            Console.WriteLine($"Amazon SQS: Estado inicial - Head: {_head}, Tail: {_tail}, Size: {_size}, Capacity: {_capacity}");
            
            if (_size == 0)
            {
                Console.WriteLine("Amazon SQS: La cola está vacía");
                Console.WriteLine("=== FIN OPERACIÓN DEQUEUE SQS ===\n");
                return "La cola está vacía";
            }
                
            // Obtener el mensaje en la posición head
            string message = _queueArray[_head];
            Console.WriteLine($"Amazon SQS: Mensaje a extraer de la posición {_head}: \"{message}\"");
            
            // Limpiar la referencia para ayudar al GC
            _queueArray[_head] = null;
            Console.WriteLine($"Amazon SQS: Referencia en posición {_head} eliminada");
            
            // Actualizar head circularmente
            _head = (_head + 1) % _capacity;
            Console.WriteLine($"Resultado: {(_head + 1) % _capacity}\nOperacion: (head: {_head} + 1) % _capacity: {_capacity}");
            Console.WriteLine($"Amazon SQS: Head actualizado a {_head}");
            Console.WriteLine($"Amazon SQS: Estado final - Head: {_head}, Tail: {_tail}, Size: {_size}, Capacity: {_capacity}");
            
            // Decrementar el tamaño
            _size--;
            Console.WriteLine($"Amazon SQS: Size decrementado a {_size}");
            
            // Si el tamaño es muy pequeño, reducir el array para ahorrar memoria
            if (_size > 0 && _size <= _capacity / 4 && _capacity > 10)
            {
                Console.WriteLine($"Amazon SQS: Optimización de tamaño activada");
                Resize(_capacity / 2);
            }
            
            DisplayQueueState();
            Console.WriteLine($"Amazon SQS: Mensaje desencolado: \"{message}\"");
            Console.WriteLine("=== FIN OPERACIÓN DEQUEUE SQS ===\n");
            return message;
        }
        
        private void Resize(int newCapacity)
        {
            Console.WriteLine($"Amazon SQS: Redimensionando array de {_capacity} a {newCapacity}");
            string[] newArray = new string[newCapacity];
            
            // Copiar los elementos al nuevo array
            for (int i = 0; i < _size; i++)
            {
                newArray[i] = _queueArray[(_head + i) % _capacity];
            }
            
            // Actualizar referencias
            _queueArray = newArray;
            _head = 0;
            _tail = _size;
            _capacity = newCapacity;
            
            Console.WriteLine($"Amazon SQS: Array redimensionado - Nuevo Head: {_head}, Nuevo Tail: {_tail}, Nueva Capacidad: {_capacity}");
        }
        
        public bool IsEmpty()
        {
            bool isEmpty = _size == 0;
            Console.WriteLine($"Amazon SQS: Verificando si está vacía - Size: {_size}, ¿Está vacía?: {isEmpty}");
            return isEmpty;
        }
        
        public int Count()
        {
            Console.WriteLine($"Amazon SQS: Contando elementos - Size: {_size}");
            return _size;
        }
        
        private void DisplayQueueState()
        {
            Console.WriteLine("Amazon SQS: Estado actual de la cola:");
            StringBuilder queueVisual = new StringBuilder();
            queueVisual.Append("FRONT [");
            
            int displayCount = Math.Min(10, _size);
            
            for (int i = 0; i < displayCount; i++)
            {
                if (i > 0) queueVisual.Append(" | ");
                queueVisual.Append(_queueArray[(_head + i) % _capacity]);
            }
            
            if (_size > 10)
                queueVisual.Append(" | ...");
                
            queueVisual.Append("] BACK");
            Console.WriteLine(queueVisual.ToString());
            Console.WriteLine($"Elementos totales: {_size}, Capacidad total: {_capacity}");
        }
        
        public void PrintQueue()
        {
            Console.WriteLine("\n=== CONTENIDO DE COLA AMAZON SQS ===");
            if (_size == 0)
            {
                Console.WriteLine("Amazon SQS: La cola está vacía");
            }
            else
            {
                DisplayQueueState();
            }
            Console.WriteLine("=== FIN CONTENIDO COLA AMAZON SQS ===\n");
        }
    }
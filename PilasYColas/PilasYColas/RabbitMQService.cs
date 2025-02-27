using System.Text;

namespace PilasYColas;

    public class RabbitMQService : IQueueService
    {
        // Usamos una lista como estructura de datos subyacente
        private List<string> _messages = new List<string>();
        private int _front = 0; // Índice del frente de la cola (para extraer)

        public int Count()
        {
            // La cantidad de elementos es la diferencia entre el tamaño de la lista y el índice frontal
            int count = _messages.Count - _front;
            Console.WriteLine($"RabbitMQ: Contando elementos - Lista.Count: {_messages.Count}, Índice frontal: {_front}, Elementos actuales: {count}");
            return count;
        }

        public string Dequeue()
        {
            Console.WriteLine("\n=== INICIO OPERACIÓN DEQUEUE ===");
            Console.WriteLine($"RabbitMQ: Estado inicial - Lista.Count: {_messages.Count}, Índice frontal: {_front}");
            
            // Verificar si hay elementos para extraer
            if (_front >= _messages.Count)
            {
                Console.WriteLine("RabbitMQ: No hay mensajes en la cola (_front >= _messages.Count)");
                Console.WriteLine("=== FIN OPERACIÓN DEQUEUE ===\n");
                return "La cola de RabbitMQ está vacía";
            }

            // Obtener el mensaje del frente sin eliminarlo todavía
            string message = _messages[_front];
            Console.WriteLine($"RabbitMQ: Mensaje encontrado en la posición {_front}: \"{message}\"");

            // Incrementar el índice frontal (esto efectivamente "elimina" el elemento)
            _front++;
            Console.WriteLine($"RabbitMQ: Índice frontal incrementado a {_front}");

            // Optimización: si el frente se ha desplazado demasiado, reconstruir la lista
            if (_front > 100 && _messages.Count > 200)
            {
                Console.WriteLine($"RabbitMQ: Optimización activada (_front > 100 && _messages.Count > 200)");
                Console.WriteLine($"RabbitMQ: Eliminando los primeros {_front} elementos");
                _messages.RemoveRange(0, _front);
                _front = 0;
                Console.WriteLine($"RabbitMQ: Lista reconstruida - Nuevo tamaño: {_messages.Count}, Nuevo índice frontal: {_front}");
            }

            DisplayQueueState();
            Console.WriteLine($"RabbitMQ: Mensaje desencolado: \"{message}\"");
            Console.WriteLine("=== FIN OPERACIÓN DEQUEUE ===\n");
            return message;
        }

        public void Enqueue(string message)
        {
            Console.WriteLine("\n=== INICIO OPERACIÓN ENQUEUE ===");
            Console.WriteLine($"RabbitMQ: Estado inicial - Lista.Count: {_messages.Count}, Índice frontal: {_front}");
            
            // Simplemente agregamos al final de la lista
            _messages.Add(message);
            Console.WriteLine($"RabbitMQ: Mensaje agregado al final de la lista: \"{message}\"");
            Console.WriteLine($"RabbitMQ: Nuevo tamaño de la lista: {_messages.Count}");
            
            DisplayQueueState();
            Console.WriteLine($"RabbitMQ: Total de mensajes en cola: {Count()}");
            Console.WriteLine("=== FIN OPERACIÓN ENQUEUE ===\n");
        }

        public bool IsEmpty()
        {
            bool isEmpty = _front >= _messages.Count;
            Console.WriteLine($"RabbitMQ: Verificando si está vacía - Lista.Count: {_messages.Count}, Índice frontal: {_front}, ¿Está vacía?: {isEmpty}");
            return isEmpty;
        }
        
        private void DisplayQueueState()
        {
            Console.WriteLine("RabbitMQ: Estado actual de la cola:");
            
            StringBuilder queueVisual = new StringBuilder();
            queueVisual.Append("FRONT [");
            
            int displayCount = Math.Min(10, Count()); // Mostrar máximo 10 elementos
            
            for (int i = 0; i < displayCount; i++)
            {
                if (i > 0) queueVisual.Append(" | ");
                queueVisual.Append(_messages[_front + i]);
            }
            
            if (Count() > 10)
                queueVisual.Append(" | ...");
                
            queueVisual.Append("] BACK");
            Console.WriteLine(queueVisual.ToString());
            Console.WriteLine($"Elementos totales: {Count()}, Elementos ocultos: {_messages.Count}");
        }
        
        public void PrintQueue()
        {
            Console.WriteLine("\n=== CONTENIDO DE COLA RABBITMQ ===");
            if (_front >= _messages.Count)
            {
                Console.WriteLine("RabbitMQ: La cola está vacía");
            }
            else
            {
                DisplayQueueState();
            }
            Console.WriteLine("=== FIN CONTENIDO COLA RABBITMQ ===\n");
        }
    }
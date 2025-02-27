using System.Text;

namespace PilasYColas;

    public class ParenthesisChecker
    {
        // Implementación manual de una pila (stack)
        private class CharStack
        {
            private char[] elements;
            private int top;
            private const int DefaultCapacity = 10;

            public CharStack()
            {
                elements = new char[DefaultCapacity];
                top = -1;
                Console.WriteLine("Stack inicializado con capacidad: " + DefaultCapacity);
            }

            public int Count => top + 1;

            public bool IsEmpty()
            {
                return top == -1;
            }

            public void Push(char item)
            {
                // Si la pila está llena, aumentar su capacidad
                if (top == elements.Length - 1)
                {
                    Console.WriteLine($"Stack lleno (capacidad: {elements.Length}). Redimensionando...");
                    ResizeArray();
                }

                elements[++top] = item;
                Console.WriteLine($"PUSH: '{item}' agregado a la pila en la posicion {top}. Elementos en pila: {Count}");
                PrintStackContent();
            }

            public char Pop()
            {
                if (IsEmpty())
                {
                    throw new InvalidOperationException("La pila está vacía");
                }

                Console.WriteLine($"removido de la pila en la posicion {top}");
                char item = elements[top--];
                Console.WriteLine($"POP: '{item}' removido de la pila. Elementos en pila: {Count}");
                PrintStackContent();
                return item;
            }

            public char Peek()
            {
                if (IsEmpty())
                {
                    throw new InvalidOperationException("La pila está vacía");
                }

                return elements[top];
            }

            private void ResizeArray()
            {
                int newSize = elements.Length * 2;
                char[] newArray = new char[newSize];
                Array.Copy(elements, newArray, elements.Length);
                elements = newArray;
                Console.WriteLine($"Stack redimensionado. Nueva capacidad: {newSize}");
            }

            private void PrintStackContent()
            {
                if (IsEmpty())
                {
                    Console.WriteLine("Stack: [vacío]");
                    return;
                }

                StringBuilder sb = new StringBuilder("Stack: [fondo] ");
                for (int i = 0; i <= top; i++)
                {
                    sb.Append(elements[i]);
                    if (i < top) sb.Append(", ");
                }
                sb.Append(" [tope]");
                Console.WriteLine(sb.ToString());
            }
        }

        public static bool EstaBalanceada(string expresion)
        {
            Console.WriteLine("\n--- Inicio verificación de paréntesis ---");
            Console.WriteLine($"Expresión a verificar: {expresion}");
            
            CharStack pila = new CharStack();

            for (int i = 0; i < expresion.Length; i++)
            {
                char c = expresion[i];
                Console.WriteLine($"\nAnalizando caracter '{c}' en posición {i}:");
                
                if (c == '(')
                {
                    Console.WriteLine($"  Encontrado paréntesis de apertura '('. Añadiendo a la pila.");
                    pila.Push(c);
                }
                else if (c == ')')
                {
                    Console.WriteLine($"  Encontrado paréntesis de cierre ')'.");
                    // Si encontramos un paréntesis de cierre y la pila está vacía,
                    // significa que hay más paréntesis de cierre que de apertura
                    if (pila.IsEmpty())
                    {
                        Console.WriteLine("  ERROR: Pila vacía al encontrar ')'. No hay paréntesis de apertura correspondiente.");
                        Console.WriteLine("--- Fin verificación: NO BALANCEADA ---\n");
                        return false;
                    }
                    Console.WriteLine("  Extrayendo paréntesis de apertura correspondiente.");
                    pila.Pop();
                }
                else
                {
                    Console.WriteLine($"  Caracter '{c}' no es un paréntesis. Ignorando.");
                }
            }

            bool resultado = pila.IsEmpty();
            if (resultado)
            {
                Console.WriteLine("--- Fin verificación: BALANCEADA ---\n");
            }
            else
            {
                Console.WriteLine("  ERROR: Quedan paréntesis de apertura sin cerrar en la pila.");
                Console.WriteLine("--- Fin verificación: NO BALANCEADA ---\n");
            }
            
            return resultado;
        }
    }
using System;
using System.Collections.Generic;

class Dragao : IComparable<Dragao>
{
    public int dias;
    public int multa;
    public double prioridade;

    public Dragao(int d, int m)
    {
        dias = d;
        multa = m;
        prioridade = (double)m / d;
    }

    public int CompareTo(Dragao other)
    {
        return prioridade.CompareTo(other.prioridade);
    }
}

class Program
{
    static void Main(string[] args)
    {
        double multadia = 0.0f; // Multa paga por dia por todos os dragões
        double multatotal = 0.0f; // Resposta
        bool treinando = false;
        int diasrestantes = 1; // Tempo restante do treinamentodo

        var fila = new PriorityQueue<Dragao>();

        while (true)
        {
            string input;
            try
            {
                input = Console.ReadLine();
                if (input == null)
                    break;

                string[] inputs = input.Split(' ');
                var tempo = int.Parse(inputs[0]);
                var multa = int.Parse(inputs[1]);

                fila.Enqueue(new Dragao(tempo, multa));
                multadia += multa;

                if(!treinando || diasrestantes != 0)
                {
                    multadia -= fila.Peek().multa;
                    diasrestantes = fila.Peek().dias;
                    fila.Pop();
                    treinando = true;
                }
                multatotal += multadia;
                diasrestantes--;
            }
            catch (FormatException)
            {
                // Final de arquivo alcançado, sair do loop
                break;
            }
        }

        if(treinando)
            multatotal += diasrestantes * multadia;

        while (fila.Count() > 0)
        {
            multadia -= fila.Peek().multa;
            multatotal += multadia * fila.Peek().dias;
            fila.Dequeue();
        }

        Console.WriteLine(multatotal.ToString("0"));
    }
}

// Implementação de uma fila de prioridade simples em C#
class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> data;

    public PriorityQueue()
    {
        this.data = new List<T>();
    }

    public void Enqueue(T item)
    {
        data.Add(item);
        int ci = data.Count - 1; // child index; start at end
        while (ci > 0)
        {
            int pi = (ci - 1) / 2; // parent index
            if (data[ci].CompareTo(data[pi]) >= 0) break; // child item is larger than (or equal) parent so we're done
            T tmp = data[ci]; data[ci] = data[pi]; data[pi] = tmp;
            ci = pi;
        }
    }

    public T Dequeue()
    {
        // assumes pq is not empty; up to calling code
        int li = data.Count - 1; // last index (before removal)
        T frontItem = data[0];   // fetch the front
        data[0] = data[li];
        data.RemoveAt(li);

        --li; // last index (after removal)
        int pi = 0; // parent index. start at front of pq
        while (true)
        {
            int ci = pi * 2 + 1; // left child index of parent
            if (ci > li) break;  // no children so done
            int rc = ci + 1;     // right child
            if (rc <= li && data[rc].CompareTo(data[ci]) < 0) // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead
                ci = rc;
            if (data[pi].CompareTo(data[ci]) <= 0) break; // parent is smaller than (or equal to) smallest child so done
            T tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp; // swap parent and child
            pi = ci;
        }
        return frontItem;
    }

    public int Count()
    {
        return data.Count;
    }

    public T Peek()
    {
        if (data.Count == 0)
            throw new InvalidOperationException("A fila de prioridade está vazia.");
        return data[0];
    }

        public T Pop()
    {
        if (data.Count == 0)
            throw new InvalidOperationException("A fila de prioridade está vazia.");
        T frontItem = data[0];   // fetch the front
        data[0] = data[data.Count - 1];
        data.RemoveAt(data.Count - 1);

        int li = data.Count - 1; // last index (before removal)

        int pi = 0; // parent index. start at front of pq
        while (true)
        {
            int ci = pi * 2 + 1; // left child index of parent
            if (ci > li) break;  // no children so done
            int rc = ci + 1;     // right child
            if (rc <= li && data[rc].CompareTo(data[ci]) < 0) // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead
                ci = rc;
            if (data[pi].CompareTo(data[ci]) <= 0) break; // parent is smaller than (or equal to) smallest child so done
            T tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp; // swap parent and child
            pi = ci;
        }
        return frontItem;
    }
}

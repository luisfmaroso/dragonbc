import heapq

class Dragao:
    def __init__(self, dias, multa):
        self.dias = dias
        self.multa = multa
        self.prioridade = multa / dias

    def __lt__(self, other):
        return self.prioridade < other.prioridade

multadia = 0.0  # Multa paga por dia por todos os dragões
multatotal = 0.0  # Resposta
treinando = False
diasrestantes = 0  # Tempo restante do treinamento
fila = []

while True:
    try:
        entrada = input().split()
        tempo, multa = map(int, entrada)
        heapq.heappush(fila, Dragao(tempo, multa))
        multatotal += multa  # Adiciona a multa ao total
        
        if not treinando or not diasrestantes:
            if fila:  # Verifica se há dragões na fila
                multadia += fila[0].multa  # Adiciona a multa do próximo dragão ao total diário
                diasrestantes = fila[0].dias
                heapq.heappop(fila)
                treinando = True

        diasrestantes -= 1
        multatotal += multadia * (diasrestantes + 1)  # Adiciona a multa diária ao total
    except EOFError:
        break
    except ValueError:
        print("Entrada inválida. Por favor, insira dois números separados por espaço.")
        continue

while fila:
    multatotal += fila[0].multa * fila[0].dias  # Adiciona a multa acumulada pelos dias de treinamento do dragão restante
    heapq.heappop(fila)

print(int(multatotal))

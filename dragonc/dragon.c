#include <stdio.h>
#include <stdlib.h>

typedef struct {
    int dias, multa;
} Dragao;

typedef struct {
    double prioridade;
    Dragao dragao;
} Node;

// Função de comparação para a fila de prioridade
int comparaNode(const void *a, const void *b) {
    const Node *nodeA = (const Node *)a;
    const Node *nodeB = (const Node *)b;
    return (nodeA->prioridade < nodeB->prioridade) - (nodeA->prioridade > nodeB->prioridade);
}

int main(void) {
    int tempo, multa;
    double multadia = 0.0; // Multa paga por dia por todos os dragões
    double multatotal = 0.0; // Resposta
    int treinando = 0;
    int diasrestantes = 0; // Tempo restante do treinamento
    Node *fila = NULL;
    int capacidade = 0;
    int tamanho = 0;

    while (!feof(stdin) && scanf("%d %d", &tempo, &multa) == 2) {
        // Alocar mais memória conforme necessário
        if (tamanho == capacidade) {
            capacidade = (capacidade == 0) ? 1 : capacidade * 2;
            fila = (Node *)realloc(fila, capacidade * sizeof(Node));
        }
        // Adicionar o novo dragão à fila de prioridade
        fila[tamanho].dragao.dias = tempo;
        fila[tamanho].dragao.multa = multa;
        fila[tamanho].prioridade = (double)multa / tempo;
        tamanho++;
        // Atualizar a fila de prioridade
        qsort(fila, tamanho, sizeof(Node), comparaNode);

        multadia += multa;
        
        // Se não estiver treinando ou não houver dias restantes de treinamento
        if (!treinando || !diasrestantes) {
            // Atualizar as variáveis
            multadia -= fila[0].dragao.multa;
            diasrestantes = fila[0].dragao.dias;
            // Remover o dragão mais prioritário da fila
            for (int i = 0; i < tamanho - 1; ++i) {
                fila[i] = fila[i + 1];
            }
            tamanho--;
            treinando = 1;
        }
        multatotal += multadia;
        diasrestantes--;
    }

    // Se ainda estiver treinando, calcular a multa total restante
    if (treinando)
        multatotal += diasrestantes * multadia;

    // Calcular a multa total para os dragões restantes na fila
    for (int i = 0; i < tamanho; ++i) {
        multadia -= fila[i].dragao.multa;
        multatotal += multadia * fila[i].dragao.dias;
    }

    // Imprimir a multa total
    printf("%.0f\n", multatotal);

    // Liberar a memória alocada para a fila de prioridade
    free(fila);
    return 0;
}
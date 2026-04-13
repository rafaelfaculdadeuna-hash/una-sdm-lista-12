Entrega de atividades relacionada a lista una-sdm-lista-12

>**Pergunta:** "Em um cenário de Sistemas Distribuídos
com alto tráfego (ex: lançamento de um tênis limitado), o que poderia
acontecer se dois clientes tentarem comprar o último par de tênis ao
exato mesmo milissegundo? Como poderíamos resolver isso?"

> **Resposta:** Se dois clientes tentarem comprar o último item ao mesmo tempo, ocorre uma condição de corrida: ambos veem o estoque disponível e conseguem finalizar a compra. Como resultado, o sistema pode vender o mesmo produto duas vezes (overselling) ou até ficar com estoque negativo.
Para tratar isso, podemos fazer uma validação diretamente na atualização do estoque no banco, garantindo que a quantidade nunca fique negativa (por exemplo, só permitir a atualização se o estoque for maior que zero). Assim, apenas uma das compras será concluída com sucesso.

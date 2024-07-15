Tema: cadastro simples de cliente de loja

Objetivo:
O objetivo é criar um conjunto de API para fazer um cadastro genérico simples para lojas. 
Estas API serão futuramente integradas a uma plataforma de apoio a lojistas, porém este trabalho tem como foco unicamente as APIs voltadas ao cadastro do cliente e ao de disparo de notificação.

Requisitos Funcionais:
1-O sistema deve possibilitar o cadastro de usuários;
2-O sistema deve inserir requisições na fila;
3-O sistema deve monitorar a fila de requisições;
4-A fila de requisições deve ser monitorada pelo sistema a cada cinco minutos.

Requisitos não funcionais:
1-Utilizar o MongoDB como banco de dados;
2-Utilizar o RabbitMQ para gerenciar filas;
3-Utilizar arquitetura limpa.

Regras de negócio:
1-Somente deverão ser aceitos números de CPFs válidos;
2-Endereços de e-mail devem ser formados pelo conjunto de pelo menos um caractere, arroba e pelo menos um caractere.

Caso de Uso:
1- A API de cadastro deve ser capaz de cadastrar um cliente genérico ao ser acionada por qualquer um dos programas de front, ao ser acionada esta deverá validar os dados a serem inseridos e posteriormente enviar para a fila de processamento.

2- A API de processamento de cadastro deve de tempos em tempos checar a fila de tarefas, ao identificar uma requisição de cadastro esta persistirá os dados no banco e enviará um SMS ao cliente informando o respectivo cadastro.

Banco de dados: 
Será utilizado o MongoDB por questões da velocidade e para simplificar a modelagem do banco que futuramente poderá conter diversas ramificações filhas de tabelas já existentes.

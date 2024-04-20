**Visão Geral do Projeto**

Este projeto foi concebido para simular um dia de trabalho de 9 horas, destacando o desenvolvimento de uma aplicação web de Gestão de Contactos Multi utilizando ASP.NET Core com uma arquitetura MVC. O projeto envolveu a configuração e personalização de um framework baseado em MVC para gerir contactos, integrando autenticação via JWT para operações seguras, e aderindo a práticas de Clean Code através de estratificação estruturada e arquitetura orientada a Objeto.

**Configuração Inicial e Abordagem**
                                                                **Análise e Preparação**

*Início:* Os primeiros 50 minutos do dia foram dedicados à leitura dos requisitos do projeto, à configuração de ferramentas necessárias como o FileZilla para acesso a dados, e ao download do projeto.

*Decisão Arquitetural:* Ao notar que o projeto estava estruturado em MVC, decidi introduzir camadas adicionais para realçar a separação de preocupações e evitar o acoplamento, principalmente através de serviços que tratam da lógica de negócio e acesso a dados.

*Fases de Desenvolvimento:* Decidi adicionar camadas de serviço para encapsular a lógica de negócio, separando-a dos controladores MVC para garantir que cada componente trata apenas das preocupações apropriadas, tentando atender ao padrão S.O.L.I.D, evitando também o encapsulamento e podendo futuramente escalonar este projeto.

Durante a primeira horá, decidi codar orientado a TDD, contudo após ter passado 2 horas, notei que não seria viavel a entrega do máximo possivel, pois era pouco tempo para implementação seguida de testes consecutivos.

Após essa decisão, comecei a fazer a implementação das camadas, desenvolvendo as Classes, posteriormente o Services, seguindo para o restante da lógica.

Decidi implementar a autenticação JWT para proteger endpoints que Criam, Modificam e Deletam recursos, assegurando que essas operações só possam ser realizadas por utilizadores autenticados.

Os testes passaram a ser feitos com a utilização de localHost atráves de servidor local e Postman para testes das rotas.

A lógica que perpetrou durante todo o projeto foi de: Entendimento das demandas, estruturação, desenvolvimento de lógica do Back End e por último integração da lógica da View que por falta de tempo acabou ficando para o próximo dia de trabalho se houvesse.

**Estrutura e Lógica do Projeto**
**CONTROLLER**
*AuthenticationController:* Gerencia a autenticação do utilizador, emitindo tokens JWT após login bem-sucedido, utilizando claims e credenciais de assinatura para proteger e validar tokens.

*PeopleController:* Trata das operações relacionadas com 'Pessoas', incluindo operações CRUD integradas com JWT para acesso seguro.

*ContactStatisticsController:* Fornece uma visão geral das estatísticas de contactos por país, demonstrando agregação de dados e consulta dentro do framework MVC.

*HomeController:* Serve as vistas básicas e dados JSON relacionados aos clientes da aplicação, incluindo tratamento de erros e configurações de privacidade.

**MODELS**
Person, Contact, Country, Customer: Modelos de entidade representando os diversos aspectos de dados geridos pela aplicação. Estes modelos incluem relações como uma pessoa que possui múltiplos contactos.

**SERVICES**
*PersonService, ContactService, CountriesService:* Estes serviços tratam da lógica de negócio para as suas respectivas áreas, como recuperar, atualizar e eliminar dados da base de dados. Interagem com o DbContext para aplicar alterações na base de dados.

*TokenService:* Gerencia a geração de tokens JWT para sessões autenticadas, encapsulando a lógica de criação de tokens.

**SECURITY**
*JWT Setup:* Configurada no Startup.cs com parâmetros de validação de token detalhados para garantir a segurança na manipulação de tokens e proteção de endpoints.

**FEATURES e FUNCTIONALITIES**

*Gestão de Contactos:* Os utilizadores podem adicionar, editar e eliminar contactos após autenticação bem-sucedida.

*Acesso Seguro:* Operações que modificam dados são protegidas com JWT, requerendo tokens válidos.

*Visualização de Dados:* Estatísticas de contactos por país são exibidas, demonstrando a capacidade da aplicação de agregar e apresentar dados.

*Autenticação de Utilizadores:* Suporta login seguro com JWT, incluindo emissão e validação de tokens.

**DESIGN e ARQUITETURA**
*Arquitetura MVC:* Adere ao padrão Modelo-Visão-Controlador para separar preocupações, melhorar a manutenção e aumentar a testabilidade.

*Arquitetura Orientada a Serviços:* Os serviços são utilizados para lidar com a lógica de negócio, reduzindo a complexidade dentro dos controladores e garantindo que as regras de negócio estejam encapsuladas dentro das camadas de serviço.

*Autenticação por Token:* Utiliza JWT para acesso seguro à API, garantindo que operações como criar, atualizar e eliminar estejam protegidas contra acessos não autorizados.

*CONCLUSÃO*
Este projeto exemplifica uma abordagem estruturada para construir uma aplicação web segura e robusta usando o framework MVC do ASP.NET Core, integrando mecanismos modernos de autenticação e seguindo as melhores práticas para arquitetura limpa. A aplicação foi projetada para ser intuitiva para os desenvolvedores pegarem, entenderem e expandirem, com clara separação de preocupações e tratamento seguro das operações do utilizador.

Esse trecho a qual alcancei de código, foi feito no prazo estipulado de código, posteriormente, se fosse um senario real de trabalho, nos próximos dias, seguindo com a demanda, até sua conclusão com sucesso em pouco tempo !
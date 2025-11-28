describe('Criação de Computador pelo Administrador', () => {
  beforeEach(() => {
    // Acessa diretamente a página do admin
    cy.visit('http://localhost:5269/Principal/IndexADM')
    
    // Verifica se carregou a página do admin
    cy.contains('Criar Sala e Computador').should('be.visible')
  })

  it('deve criar um computador C01-LAB1-5605 no LAB I', () => {
    // 1. Acessa "Criar Sala e Computador"
    cy.contains('Criar Sala e Computador').click()
    cy.url().should('include', '/Computador/CriarPC')
    cy.contains('Criar Computador').should('be.visible')

    // 2. Preenche o nome do computador
    cy.get('#nomePc')
      .should('be.visible')
      .type('C01-LAB1-5605')

    // 3. Seleciona o LAB I na lista de salas
    cy.get('#salaPc')
      .should('be.visible')
      .select('Lab I')

    // 4. Verifica se o botão de enviar está habilitado
    // (O botão pode estar desabilitado até que os campos obrigatórios sejam preenchidos)
    cy.get('#btnEnviarPc').should('not.be.disabled')

    // 5. Envia o formulário
    cy.get('#btnEnviarPc').click()

    // 6. Verifica se foi redirecionado ou se a criação foi bem sucedida
    // Isso depende do comportamento após o envio - pode voltar para IndexADM ou mostrar mensagem
    cy.url().should('not.include', '/CriarPC')
    // Ou verifica mensagem de sucesso se houver
  })
})
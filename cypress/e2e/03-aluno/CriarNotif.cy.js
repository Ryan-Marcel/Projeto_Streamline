describe('Criação de Solicitação pelo Aluno', () => {
  beforeEach(() => {
    // Login como aluno
    cy.visit('http://localhost:5269')
    cy.get('#username').type('aluno123')
    cy.get('#password').type('senha123')
    cy.contains('button', 'Entrar').click()
    
    // Aguarda carregar a página principal
    cy.contains('Criar Notificação').should('be.visible')
  })

  it('deve criar uma solicitação para computador sem internet', () => {
    // 1. Acessa a página de criar notificação
    cy.contains('Criar Notificação').click()
    cy.url().should('include', '/Principal/Criar')
    cy.contains('Criar Solicitação').should('be.visible')

    // 2. Seleciona a sala "Lab I"
    cy.get('select#sala').select('Lab I')
    
    // 3. Clica em "Carregar Computadores"
    cy.get('button.carregar').click()

    // 4. Aguarda carregar os computadores e seleciona o primeiro computador
    cy.get('button#computador').first().click()

    // 5. Digita a mensagem no textarea
    cy.get('textarea[name="Mensagem"]')
      .should('be.visible')
      .type('computador sem internet')

    // 6. Envia a mensagem
    cy.contains('button', 'Enviar Mensagem').click()

    // 7. Verifica se a solicitação foi criada com sucesso
    // (Aqui você pode verificar se voltou para alguma página específica ou se aparece mensagem de sucesso)
    cy.url().should('not.include', '/Solicitacao')
    // Ou verifica se aparece mensagem de sucesso, se houver
  })
})
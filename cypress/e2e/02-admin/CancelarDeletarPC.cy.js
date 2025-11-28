describe('Cancelamento de Exclusão de Computador', () => {
  beforeEach(() => {
    cy.visit('http://localhost:5269/Principal/IndexADM')
    cy.contains('Lista de Notificações').should('be.visible')
  })

  it('deve cancelar a exclusão do computador C01-LAB1-5605 clicando em Não', () => {
    // 1. Navega até a página de edição
    cy.contains('Lista de Notificações').click()
    cy.get('#editar').contains('Editar').click()

    // 2. Encontra e clica no botão de deletar do computador C01-LAB1-5605
    cy.contains('li', 'Nome: C01-LAB1-5605')
      .find('.btn-delete')
      .click()

    // 3. Verifica página de confirmação
    cy.url().should('include', '/Principal/Deletar')
    cy.contains('Você tem certeza que deseja DELETAR o computador').should('be.visible')

    // 4. Cancela a exclusão
    cy.contains('a', 'Não').click()

    // 5. Verifica se voltou para edição e o computador ainda existe
    cy.url().should('include', '/Principal/Editar')
    cy.contains('Nome: C01-LAB1-5605').should('be.visible')
  })
})
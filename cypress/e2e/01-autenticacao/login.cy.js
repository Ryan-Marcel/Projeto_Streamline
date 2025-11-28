

describe('Login sem credenciais', () => {
  it('deve fazer login sem escrever', () => {
    // 1. Acessa a página de login
    cy.visit('http://localhost:5269')
    // 2. Clica no botão Entrar sem preencher nada  
    cy.contains('button', 'Entrar').click()
    
    cy.url().should('not.include', '/login')
  })
})

  describe('Teste de Login Básico', () => {
  it('deve fazer login como aluno', () => {
    // 1. Acessa a página de login
    cy.visit('http://localhost:5269')

    // 2. Preenche o usuário
    cy.get('#username').type('aluno123')
    
    // 3. Preenche a senha  
    cy.get('#password').type('senha123')
    
    // 4. Clica no botão Entrar
    cy.contains('button', 'Entrar').click()
    
    // 5. Verifica se saiu da página de login
    cy.url().should('not.include', '/login')
  })
})


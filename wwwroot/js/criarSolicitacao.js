document.addEventListener('DOMContentLoaded', function() {
    const salaSelect = document.getElementById('salaSelect');
    const btnProximoSala = document.getElementById('btnProximoSala');
    const btnVoltarSala = document.getElementById('btnVoltarSala');
    const btnProximoComputador = document.getElementById('btnProximoComputador');
    const btnVoltarComputador = document.getElementById('btnVoltarComputador');
    const btnEnviar = document.getElementById('btnEnviar');
    const mensagemTextarea = document.getElementById('mensagemTextarea');
    const mensagemHint = document.getElementById('mensagemHint');

    const stepSala = document.getElementById('step-sala');
    const stepComputador = document.getElementById('step-computador');
    const stepMensagem = document.getElementById('step-mensagem');

    const problemasCheckboxes = document.querySelectorAll('input[name="ProblemaRecorrente"]');
    const resumoProblemasContainer = document.getElementById('resumo-problemas-container');
    const resumoProblemas = document.getElementById('resumo-problemas');

    // Validação Sala
    salaSelect.addEventListener('change', function() {
        const isValid = this.value !== '0';
        btnProximoSala.disabled = !isValid;
        btnProximoSala.style.opacity = isValid ? '1' : '0.6';
    });

    // Validação inicial
    if (salaSelect.value !== '0') {
        btnProximoSala.disabled = false;
        btnProximoSala.style.opacity = '1';
    }

    // Próximo: Sala -> Computador
    btnProximoSala.addEventListener('click', function() {
        const salaId = salaSelect.value;
        if (salaId === '0') return;

        carregarComputadores(salaId);
        
        stepSala.classList.remove('active');
        stepComputador.classList.add('active');
        window.scrollTo({ top: 0, behavior: 'smooth' });
    });

    // Voltar: Computador -> Sala
    btnVoltarSala.addEventListener('click', function() {
        stepComputador.classList.remove('active');
        stepSala.classList.add('active');
        window.scrollTo({ top: 0, behavior: 'smooth' });
    });

    // Validação Computador
    document.addEventListener('change', function(e) {
        if (e.target.name === 'ComputadorId') {
            btnProximoComputador.disabled = false;
            btnProximoComputador.style.opacity = '1';
            
            document.querySelectorAll('.computador-card').forEach(card => {
                card.classList.remove('selected');
            });
            e.target.closest('.computador-card').classList.add('selected');
        }
    });

    // Próximo: Computador -> Mensagem
    btnProximoComputador.addEventListener('click', function() {
        const computadorSelecionado = document.querySelector('input[name="ComputadorId"]:checked');
        if (!computadorSelecionado) return;

        atualizarResumo();

        stepComputador.classList.remove('active');
        stepMensagem.classList.add('active');
        window.scrollTo({ top: 0, behavior: 'smooth' });
        
        validarFormulario();
    });

    // Voltar: Mensagem -> Computador
    btnVoltarComputador.addEventListener('click', function() {
        stepMensagem.classList.remove('active');
        stepComputador.classList.add('active');
        window.scrollTo({ top: 0, behavior: 'smooth' });
    });

    // LÓGICA DOS PROBLEMAS RECORRENTES
    problemasCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function() {
            const algumSelecionado = Array.from(problemasCheckboxes).some(cb => cb.checked);
            
            if (algumSelecionado) {
                // Preenche textarea PRIMEIRO
                const problemasSelecionados = Array.from(problemasCheckboxes)
                    .filter(cb => cb.checked)
                    .map(cb => cb.value);
                mensagemTextarea.value = 'Problemas relatados:\n' + problemasSelecionados.join('\n');
                
                // Depois desabilita visualmente
                mensagemTextarea.disabled = true;
                mensagemTextarea.style.backgroundColor = '#f0f0f0';
                mensagemTextarea.style.cursor = 'not-allowed';
                mensagemTextarea.placeholder = 'Descrição desabilitada (problemas recorrentes selecionados)';
                mensagemHint.textContent = 'Problemas selecionados serão enviados automaticamente';
            } else {
                // Limpa e reabilita
                mensagemTextarea.value = '';
                mensagemTextarea.disabled = false;
                mensagemTextarea.style.backgroundColor = '#f8f9fa';
                mensagemTextarea.style.cursor = 'text';
                mensagemTextarea.placeholder = 'Descreva detalhadamente o problema que você está enfrentando...';
                mensagemHint.textContent = 'Quanto mais detalhes você fornecer, mais rápido poderemos ajudar';
            }
            
            atualizarResumoProblemas();
            validarFormulario();
        });
    });

    // Validação da mensagem manual
    mensagemTextarea.addEventListener('input', function() {
        validarFormulario();
    });
    
    // CRÍTICO: Antes de enviar o form, reabilitar textarea para o valor ser enviado
    document.getElementById('formSolicitacao').addEventListener('submit', function(e) {
        const algumProblema = Array.from(problemasCheckboxes).some(cb => cb.checked);
        
        if (algumProblema) {
            // Reabilita temporariamente para o form enviar o valor
            mensagemTextarea.disabled = false;
            
            // Garante que o valor está preenchido
            if (mensagemTextarea.value.trim() === '') {
                const problemasSelecionados = Array.from(problemasCheckboxes)
                    .filter(cb => cb.checked)
                    .map(cb => cb.value);
                mensagemTextarea.value = 'Problemas relatados:\n' + problemasSelecionados.join('\n');
            }
        }
    });

    // Função de validação do formulário
    function validarFormulario() {
        const algumProblema = Array.from(problemasCheckboxes).some(cb => cb.checked);
        const mensagemPreenchida = mensagemTextarea.value.trim() !== '';
        
        const isValid = algumProblema || mensagemPreenchida;
        
        btnEnviar.disabled = !isValid;
        btnEnviar.style.opacity = isValid ? '1' : '0.6';
    }

    // Função para atualizar resumo de problemas
    function atualizarResumoProblemas() {
        const problemasSelecionados = Array.from(problemasCheckboxes)
            .filter(cb => cb.checked)
            .map(cb => cb.value);
        
        if (problemasSelecionados.length > 0) {
            resumoProblemasContainer.style.display = 'flex';
            resumoProblemas.textContent = problemasSelecionados.join(', ');
        } else {
            resumoProblemasContainer.style.display = 'none';
        }
    }

    // Função para carregar computadores
    function carregarComputadores(salaId) {
        const listaComputadores = document.getElementById('computadores-lista');
        listaComputadores.innerHTML = '<div class="loading">Carregando computadores...</div>';

        fetch(`/Principal/Solicitacao?SalaId=${salaId}`)
            .then(response => response.text())
            .then(html => {
                listaComputadores.innerHTML = html;
            })
            .catch(error => {
                console.error('Erro ao carregar computadores:', error);
                listaComputadores.innerHTML = '<div class="erro">Erro ao carregar computadores. Tente novamente.</div>';
            });
    }

    // Função para atualizar resumo
    function atualizarResumo() {
        const salaTexto = salaSelect.options[salaSelect.selectedIndex].text;
        const computadorSelecionado = document.querySelector('input[name="ComputadorId"]:checked');
        const computadorLabel = computadorSelecionado ? 
            computadorSelecionado.closest('.computador-card').querySelector('strong').textContent : '-';

        document.getElementById('resumo-sala').textContent = salaTexto;
        document.getElementById('resumo-computador').textContent = computadorLabel;
        
        atualizarResumoProblemas();
    }

    // Auto-hide alerts
    setTimeout(function() {
        const alerts = document.querySelectorAll('.alert');
        alerts.forEach(alert => {
            alert.style.transition = 'opacity 0.5s';
            alert.style.opacity = '0';
            setTimeout(() => alert.remove(), 500);
        });
    }, 5000);
});
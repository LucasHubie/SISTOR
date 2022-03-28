<template>
  <div>

    <b-alert :show="dismissCountDown"
             dismissible
             variant="warning"
             @dismissed="dismissCountDown=0"
             @dismiss-count-down="countDownChanged">
      Confirmado com sucesso! <!--{{ dismissCountDown }}-->
    </b-alert>

    <b-card style="box-shadow: 3px 0px 5px 3px #0000007d;" no-body>
      <b-card-header class="border-0">
        <h3 class="mb-0 float-left">Funcionários</h3>

        <base-button v-b-modal.modal-scrollable type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v:onclick="">
          <b-icon icon="question-circle-fill" aria-label="Help"></b-icon>
        </base-button>

        <base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v-on:click="showFiltrar = !showFiltrar">
          <b-icon icon="filter-square-fill" font-scale="1"></b-icon>
          <span class="btn-inner--text">Filtrar</span>
        </base-button>

        <base-button v-b-modal.modal-1 type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v:onclick="resetModal">
          <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
          <span class="btn-inner--text">Adicionar</span>
        </base-button>

      </b-card-header>

      <div v-if="showFiltrar" class="modal-body">
        <b-row>
          <b-col lg="3">
            <base-input type="text"
                        label="Pesquisar"
                        placeholder="Buscar por..."
                        v-model="filtro.nome">
            </base-input>
          </b-col>
          <b-col lg="1">
            <base-button class="float-right" style="background-color: rgb(58 99 167); margin: 0; position: absolute; top: 53%; -ms-transform: translateY(-50%); transform: translateY(-50%);" v-on:click="BuscarFuncionario">
              <span class="btn-inner--text">Filtrar</span>
            </base-button>
          </b-col>

        </b-row>
      </div>

      <el-table class="table-responsive table"
                header-row-class-name="thead-light"
                :data="funcionarios">


        <el-table-column label="Nome"
                         min-width="250px"
                         prop="name">
          <template v-slot="{row}">
            <b-media no-body class="align-items-center">
              <b-media-body>
                <span class="font-weight-600 name mb-0 text-sm">{{row.pessoa.nome}}</span>
              </b-media-body>
            </b-media>
          </template>
        </el-table-column>

        <el-table-column label="E-mail"
                         prop="pessoa.email"
                         min-width="250px">
        </el-table-column>
        <el-table-column label="Telefone"
                         prop="pessoa.telefone"
                         min-width="160px">
        </el-table-column>
        <el-table-column label="Celular"
                         prop="pessoa.celular"
                         min-width="160px">
        </el-table-column>
        <el-table-column label="Ações"
                         min-width="200px"
                         prop="">
          <template v-slot="{row}">

            <base-button v-on:click="getFuncionarioById(row.id, 'Visualizar')" v-b-modal.modal-3 size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="eye-fill" font-scale="1"></b-icon></base-button>
            <base-button v-on:click="getFuncionarioById(row.id, 'Alterar')" size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="pencil-fill" font-scale="1"></b-icon></base-button>
            <base-button v-on:click="deleteFuncionarioConfirmed(row.id)" size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="trash-fill" font-scale="1"></b-icon></base-button>

          </template>
        </el-table-column>

      </el-table>

      <b-card-footer class="py-4 d-flex justify-content-end">
        <base-pagination v-model="currentPage" :per-page="10" :total="qntdRegistros" @change="change"></base-pagination>
      </b-card-footer>

      <!--Modal Ajuda-->
      <b-button v-b-modal.modal-scrollable></b-button>

      <b-modal id="modal-scrollable" scrollable title="Tela de Funcionários">
        <p class="my-4" v-for="i in 1" :key="i">
          Para inclusão de um novo funcionário basta clicar sobre o botão "Adicionar" e preencher todos os campos com os dados sobre o mesmo.
          <br />
          <br />
          Ao lado do botão de inclusão, temos o botão "Filtrar" que ao clicado abre uma caixa de pesquisa onde pode-se buscar por diferentes opções, como nome, e-mail,
          cpf, rg e outros.
          <br />
          <br />
          Um pouco mais abaixo temos uma listagem de todos os funcionários já inclusos no sistema, caso esteja em branco, isso se da por não haver nenhum funcionário incluso até o momento.
          <br />
          <br />
          Dentro dessa listagem sobre cada funcionário, temos 3 opções em cada um deles. Começando da esquerda para a direita, temos o botão de detalhes, representado pelo ícone do olho,
          onde após clicado será mostrado todos os detalhes do funcionário, visto que na tela inicial não cabem todos os detalhes do mesmo.
          <br />
          <br />
          Na sequência temos o botão do meio que se refere ao botão de alteração do funcionário, representado pelo ícone de lápis. Então clicando sobre ele, será carregado todos os dados
          do funcionário para alteração.
          <br />
          <br />
          E por último temos o botão de exclusão, representado pelo ícone da lata de lixo. Onde após clicado, abrirá uma tela de confirmação de exclusão, onde terá a opção de confirmar
          ou não a exclusão do funcionário.
        </p>

        <template #modal-footer>
          <b-row>
            <b-col lg="12">
              <base-button type="success" class="float-right" style="margin-right: 10px;" v-on:click="cancelarHelp">
                <span class="btn-inner--text">Fechar</span>
              </base-button>
              
            </b-col>
          </b-row>
        </template>

      </b-modal>

      <!--Modal inclusão-->
      <b-modal id="modal-1" title="Funcionário" size="xl" 
               @hidden="resetModal"
               @ok="handleOk">

        <form ref="form" id="FormFuncionario" @submit.stop.prevent="handleSubmit">

          <b-form-group>

            <h6 class="heading-small text-muted mb-4">Informações do Funcionário</h6>

            <h5 class="redHeading">* Indica item obrigatório (todos os campos obrigatórios devem ser preenchidos)</h5>

            <div class="">

              <b-row>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="Nome*"
                                label-for="name-input"
                                invalid-feedback="Nome é obrigatório"
                                :state="nameState">
                    <b-form-input id="name-input"
                                  placeholder="Nome"
                                  v-model="funcionario.pessoa.nome"
                                  :state="nameState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="CPF*"
                                label-for="cpf-input"
                                invalid-feedback="CPF é obrigatório"
                                :state="cpfState">
                     <b-form-input id="cpf-input"
                                placeholder="000.000.000-00"
                                v-mask="'###.###.###-##'"
                                v-model="funcionario.pessoa.cpf"
                                :state="cpfState"
                                required>

                     </b-form-input>

                  </b-form-group>
                </b-col>

              </b-row>
              <b-row>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="RG*"
                                label-for="rg-input"
                                invalid-feedback="RG é obrigatório"
                                :state="rgState">
                    <b-form-input id="rg-input"
                                  placeholder="RG"
                                  v-model="funcionario.pessoa.rg"
                                  :state="rgState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>


                <b-col lg="6">

                  <b-form-group label="E-mail*"
                                label-for="email-input"
                                invalid-feedback="E-mail é obrigatório"
                                :state="emailState">
                    <b-form-input id="email-input"
                                  placeholder="E-mail"
                                  v-model="funcionario.pessoa.email"
                                  :state="emailState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="Telefone"
                              placeholder="Telefone"
                              v-model="funcionario.pessoa.telefone">
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Celular"
                              placeholder="Celular"
                              v-model="funcionario.pessoa.celular">
                  </base-input>
                </b-col>
              </b-row>
              <b-row>
                <b-col lg="6">

                  <b-form-group label="Cidade*"
                                label-for="cidade-input"
                                invalid-feedback="Cidade é obrigatória"
                                :state="cidadeState">
                    <b-form-input id="cidade-input"
                                  placeholder="Cidade"
                                  v-model="funcionario.pessoa.cidade"
                                  :state="cidadeState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>
                <b-col lg="3">
                  <b-form-group label="UF*"
                                label-for="uf-input"
                                invalid-feedback="UF é obrigatório"
                                :state="ufState">
                    <b-form-select v-model="funcionario.pessoa.uf"
                                   :state="ufState"
                                   required
                                   :options="options">

                    </b-form-select>
                  </b-form-group>
                </b-col>
              </b-row>
              <b-row>

                <b-col lg="6">

                  <b-form-group label="CEP*"
                                label-for="cep-input"
                                invalid-feedback="CEP é obrigatório"
                                :state="cepState">
                    <b-form-input id="cep-input"
                                  placeholder="CEP"
                                  v-model="funcionario.pessoa.cep"
                                  :state="cepState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>

                <b-col lg="6">

                  <b-form-group label="Endereco*"
                                label-for="endereco-input"
                                invalid-feedback="Endereço é obrigatório"
                                :state="enderecoState">
                    <b-form-input id="endereco-input"
                                  placeholder="Endereço"
                                  v-model="funcionario.pessoa.endereco"
                                  :state="enderecoState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="4">
                  <base-input type="text"
                              label="Número"
                              placeholder="Número"
                              v-model="funcionario.pessoa.numero">
                  </base-input>
                </b-col>

              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="Referência"
                              placeholder="Referência"
                              v-model="funcionario.pessoa.referencia">
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Complemento"
                              placeholder="Complemento"
                              v-model="funcionario.pessoa.complemento">
                  </base-input>
                </b-col>
              </b-row>
              <hr class="my-4">

            </div>

          </b-form-group>

        </form>

        <template #modal-footer>
          <b-row>
            <b-col lg="12">
              <base-button type="success" class="float-right" style="margin-right: 10px;" v-on:click="sendForm()">
                <span class="btn-inner--text">Confirmar</span>
                
              </base-button>
              <base-button type="secondary" class="float-right" style="margin-right: 10px;" v-on:click="cancelarInclusao">

                <span class="btn-inner--text">Cancelar</span>
              </base-button>
            </b-col>
          </b-row>
        </template>

      </b-modal>


      <!--Modal detail-->
      <b-modal id="modal-3" title="Detalhes do Funcionário" size="xl" @show="resetModal"
               @hidden="resetModal"
               @ok="handleOKdetail">

        <form ref="form" @submit.stop.prevent="handleSubmitDetail">

          <b-form-group>

            <h6 class="heading-small text-muted mb-4">Informações do Funcionário</h6>

            <div class="">

              <b-row>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="Nome"
                                label-for="name-input"
                                invalid-feedback="Nome é obrigatório"
                                :state="nameState">
                    <b-form-input id="name-input"
                                  placeholder="Nome"
                                  v-model="funcionario.pessoa.nome"
                                  :state="nameState"
                                  :disabled="disable"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="CPF"
                                label-for="cpf-input"
                                invalid-feedback="CPF é obrigatório"
                                :state="cpfState">
                    <b-form-input id="cpf-input"
                                  placeholder="000.000.000-00"
                                  v-model="funcionario.pessoa.cpf"
                                  :state="cpfState"
                                  :disabled="disable"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="RG"
                                label-for="rg-input"
                                invalid-feedback="RG é obrigatório"
                                :state="rgState">
                    <b-form-input id="rg-input"
                                  placeholder="RG"
                                  v-model="funcionario.pessoa.rg"
                                  :state="rgState"
                                  :disabled="disable"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>


                <b-col lg="6">

                  <b-form-group label="E-mail"
                                label-for="email-input"
                                invalid-feedback="E-mail é obrigatório"
                                :state="emailState">
                    <b-form-input id="email-input"
                                  placeholder="E-mail"
                                  v-model="funcionario.pessoa.email"
                                  :state="emailState"
                                  :disabled="disable"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="Telefone"
                              placeholder="Telefone"
                              :disabled="disable"
                              v-model="funcionario.pessoa.telefone">
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Celular"
                              placeholder="Celular"
                              :disabled="disable"
                              v-model="funcionario.pessoa.celular">
                  </base-input>
                </b-col>
              </b-row>
              <b-row>
                <b-col lg="6">

                  <b-form-group label="Cidade"
                                label-for="cidade-input"
                                invalid-feedback="Cidade é obrigatória"
                                :state="cidadeState">
                    <b-form-input id="cidade-input"
                                  placeholder="Cidade"
                                  v-model="funcionario.pessoa.cidade"
                                  :state="cidadeState"
                                  :disabled="disable"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>
                <b-col lg="3">
                  <b-form-group label="UF"
                                label-for="uf-input"
                                invalid-feedback="UF é obrigatório"
                                :state="ufState">
                    <b-form-select v-model="funcionario.pessoa.uf"
                                   :state="ufState"
                                   :disabled="disable"
                                   required
                                   :options="options">

                    </b-form-select>
                  </b-form-group>
                </b-col>
              </b-row>
              <b-row>

                <b-col lg="6">

                  <b-form-group label="CEP"
                                label-for="cep-input"
                                invalid-feedback="CEP é obrigatório"
                                :state="cepState">
                    <b-form-input id="cep-input"
                                  placeholder="CEP"
                                  v-model="funcionario.pessoa.cep"
                                  :state="cepState"
                                  :disabled="disable"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>

                <b-col lg="6">

                  <b-form-group label="Endereco"
                                label-for="endereco-input"
                                invalid-feedback="Endereço é obrigatório"
                                :state="enderecoState">
                    <b-form-input id="endereco-input"
                                  placeholder="Endereço"
                                  v-model="funcionario.pessoa.endereco"
                                  :state="enderecoState"
                                  :disabled="disable"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="4">
                  <base-input type="text"
                              label="Número"
                              placeholder="Número"
                              :disabled="disable"
                              v-model="funcionario.pessoa.numero">
                  </base-input>
                </b-col>

              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="Referência"
                              placeholder="Referência"
                              :disabled="disable"
                              v-model="funcionario.pessoa.referencia">
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Complemento"
                              placeholder="Complemento"
                              :disabled="disable"
                              v-model="funcionario.pessoa.complemento">
                  </base-input>
                </b-col>
              </b-row>
              <hr class="my-4">


            </div>


          </b-form-group>

        </form>

        <template #modal-footer>
          <b-row>
            <b-col lg="12">
              <base-button type="success" class="float-right" style="margin-right: 10px;" v-on:click="handleOKdetail">
                <span class="btn-inner--text">Fechar</span>
              </base-button>
              
            </b-col>
          </b-row>
        </template>

      </b-modal>

    </b-card>
  </div>


</template>

<script>
  import axios from 'axios'
  import products from '../products'
  import { Table, TableColumn, DropdownMenu, DropdownItem, Dropdown } from 'element-ui'
  export default {
    name: 'table-funcionarios',
    components: {
      [Table.name]: Table,
      [TableColumn.name]: TableColumn,
      [Dropdown.name]: Dropdown,
      [DropdownItem.name]: DropdownItem,
      [DropdownMenu.name]: DropdownMenu,
    },
    data() {
      return {
        funcionarios: [],
        products,
        uf: [],
        currentPage: 1,
        selected: 'F',
        nameState: null,
        cidadeState: null,
        emailState: null,
        cpfState: null,
        rgState: null,
        ufState: null,
        cepState: null,
        enderecoState: null,
        dismissSecs: 5,
        dismissCountDown: 0,
        disable: false,
        showFiltrar: false,
        filtro: { nome: '' },
        qntdRegistros: 0,
        tpOperacao: "Incluir",
        
        funcionario: {
          pessoa: {
            nome: "",
            rg: "",
            cpf: "",
            telefone: "",
            celular: "",
            cidade: "",
            email: "",
            cep: "",
            numero: 0,
            complemento: "",
            referencia: "",
            endereco: "",
            tipoPessoa: 0,
            uf: "",
          }
        },
        orcamento: {
          descricao: 'Motor WEG'
        },
        produto: {

        },
        options: [
          { value: "AL", text: "Alagoas" },
          { value: "AC", text: "Acre" },
          { value: "AP", text: "Amapa" },
          { value: "AM", text: "Amazonas" },
          { value: "BA", text: "Bahia" },
          { value: "CE", text: "Ceará" },
          { value: "DF", text: "Distrito Federal" },
          { value: "ES", text: "Espirito Santo" },
          { value: "GO", text: "Goias" },
          { value: "MA", text: "Maranhao" },
          { value: "MT", text: "Mato Grosso" },
          { value: "MS", text: "Mato Grosso do Sul" },
          { value: "MG", text: "Minas Gerais" },
          { value: "PR", text: "Parana" },
          { value: "PB", text: "Paraiba" },
          { value: "PA", text: "Para" },
          { value: "PE", text: "Pernambuco" },
          { value: "RJ", text: "Rio de Janeiro" },
          { value: "RS", text: "Rio Grande do Sul" },
          { value: "RR", text: "Roraima" },
          { value: "RO", text: "Rondonia" },
          { value: "SP", text: "São Paulo" },
          { value: "SC", text: "Santa Catarina" },
          { value: "SE", text: "Sergipe" },
          { value: "TO", text: "Tocantins" }
        ],
        showNovoProduto: false
      };
    },
    methods: {

      countDownChanged(dismissCountDown) {
        this.dismissCountDown = dismissCountDown
      },
      showAlert() {
        this.dismissCountDown = this.dismissSecs
      },

      checkFormValidity() {
        const valid = this.$refs.form.checkValidity()
        this.nameState = valid
        this.cidadeState = valid
        this.emailState = valid
        this.cpfState = valid
        this.rgState = valid
        this.ufState = valid
        this.cepState = valid
        this.enderecoState = valid
        return valid
      },

      resetModal() {
        this.funcionario.pessoa.id = 0
        this.funcionario.pessoa.nome = ''
        this.funcionario.pessoa.cpf = ''
        this.funcionario.pessoa.celular = ''
        this.funcionario.pessoa.cep = ''
        this.funcionario.pessoa.complemento = ''
        this.funcionario.pessoa.email = ''
        this.funcionario.pessoa.endereco = ''
        this.funcionario.pessoa.numero = 0
        this.funcionario.pessoa.uf = ''
        this.funcionario.pessoa.referencia = ''
        this.funcionario.pessoa.rg = ''
        this.funcionario.pessoa.telefone = ''
        this.funcionario.pessoa.cidade = ''
        this.nameState = null
        this.cidadeState = null
        this.emailState = null
        this.cpfState = null
        this.rgState = null
        this.ufState = null
        this.cepState = null
        this.enderecoState = null
        this.tpOperacao = 'Incluir'
      },
      tipoPessoa() {
          this.Funcionario.Pessoa.TipoPessoa = 1
      },
      handleOk(bvModalEvt) {
        // Prevent modal from closing
        bvModalEvt.preventDefault()
        // chama função tipoPessoa
        this.tipoPessoa()
        // Trigger submit handler
        this.handleSubmit()
      },
      
      handleOKdetail(bvModalEvt) {
        // Prevent modal from closing
        bvModalEvt.preventDefault()
        // Trigger submit handler
        this.handleSubmitDetail()
      },
      handleSubmit() {
        // Exit when the form isn't valid
        if (!this.checkFormValidity()) {
          return
        }
        // chama função post
        this.sendForm();
        // Hide the modal manually
        this.$nextTick(() => {
          this.$bvModal.hide('modal-1')
        })
      },
      
      handleSubmitDetail() {
        this.$nextTick(() => {
          this.$bvModal.hide('modal-3')
        })
      },
      cancelarInclusao() {
        this.resetModal()
        this.$nextTick(() => {
          this.$bvModal.hide('modal-1')
        })
      },
      
      cancelarDetail() {
        this.resetModal()
        this.$nextTick(() => {
          this.$bvModal.hide('modal-3')
        })
      },
      cancelarHelp() {
        this.$nextTick(() => {
          this.$bvModal.hide('modal-scrollable')
        })
      },

      getFuncionarios(pageN, pageS) {
        axios.get("https://localhost:44376/Funcionario/Index", {
          params: { "pageNumber": pageN, "pageSize": pageS }
        }).then(response => {
          console.log(response.data)
          this.funcionarios = response.data.lst;
          this.qntdRegistros = response.data.qntdRegistros;
          console.log('carrega funcionários', this.funcionarios)
          console.log('qutnd', this.qntdRegistros)
        })
          .catch(function (error) {
            alert("Falha ao Carregar Funcionários");
          });
      },

      getFuncionarioById(id, tpOperacao) {
        if (tpOperacao == "Visualizar")
        {
          this.disable = true
        }
        
        axios.get("https://localhost:44376/Funcionario/GetFuncionarioById", {
          params: { "id": id }
        }).then(response => {
          console.log(response.data)
          this.funcionario = response.data.funcionario
          this.funcionario.pessoa = response.data.funcionario.pessoa
          console.log(this.Funcionario)

          if (tpOperacao == "Alterar") {
            this.$bvModal.show("modal-1");
            this.tpOperacao = tpOperacao
          }

        })
          .catch(function (error) {
            alert(error);
          });
      },

      BuscarFuncionario() {
        var busca = this.filtro.nome
        axios.get("https://localhost:44376/Funcionario/BuscarFuncionario", {
          params: { "busca": busca }
        }).then(response => {
          if (busca != "") {
            this.funcionarios = response.data.funcionario
          } else {
            this.getFuncionarios(1, 10)
          }
        })
          .catch(function (error) {
            alert(error);
          });
      },

      sendForm() {
        var url = "https://localhost:44376/Funcionario/CriarFuncionario"
        if (this.tpOperacao == "Alterar") {
          url = "https://localhost:44376/Funcionario/EditarFuncionario";
        }
        axios.post(url, this.funcionario).then(response => {
          if (response.data.sucess == true) {
            console.log(response.data)
            //alert(response.data.description)
            //window.location.href = "#/funcionarios"
            this.showAlert();
            this.$bvModal.hide("modal-1")
            this.getFuncionarios(1, 10)
          }
          else {
            alert(response.data.description)
          }
        })
          .catch(function (error) {
            alert(error);
          });
        // this will be called only after form is valid. You can do api call here to login
      },

      deleteFuncionarioConfirmed(id) {
        const found = this.funcionarios.find(element => element.id === id);
        this.$bvModal.msgBoxConfirm('Por favor confirme a exclusão do cliente ' + found.pessoa.nome, {
          title: 'Deseja excluir este cliente ' + found.pessoa.nome + '?',
          size: 'sm',
          buttonSize: 'sm',
          okVariant: 'danger',
          okTitle: 'Sim',
          cancelTitle: 'Não',
          footerClass: 'p-2',
          hideHeaderClose: false,
          centered: true
        })
          .then(value => {
            this.boxTwo = value
            console.log(value)
            if (this.boxTwo == true) {
              this.deleteFuncionario(id)
            } else {
              this.cancelTitle
            }
          })
          .catch(err => {
            // An error occurred
          })
      },

      deleteFuncionario(id) {
        axios.get("https://localhost:44376/Funcionario/ExcluirFuncionario", {
          params: { "id": id }
        }).then(response => {
          if (response.data.sucess == true) {
            console.log(response.data)
            /*alert(response.data.description)*/
            this.showAlert()
            this.getFuncionarios(1, 10)
          }
          else {
            alert(response.data.description)
          }
        })
          .catch(function (error) {
            alert(error);
          });
      },

      updateFuncionario() {
        axios.post("https://localhost:44376/Funcionario/EditarFuncionario", {
          Pessoa: this.Funcionario.Pessoa,
          id: this.Funcionario.id
        }).then(response => {
          if (response.data.sucess == true) {
            console.log(response.data)
            //alert(response.data.description)
            this.showAlert()
            this.$bvModal.hide("modal-2")
            this.getFuncionarios(1, 10)
          }
          else {
            alert(response.data.description)
          }
        })
          .catch(function (error) {
            alert(error);
          });
      },
      change(val) {
        console.log("teste")
        this.getFuncionarios(val, 10);
      },
    },
    mounted() {
      this.getFuncionarios(1, 10);
    }
  }
</script>

<style>
  input[type], input[type="*"] {
    color: black
  }

  .form-control {
    color: black !important;
  }
</style>

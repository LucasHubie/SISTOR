<template>
  <div>
    <b-card style="box-shadow: 3px 0px 5px 3px #0000007d;" no-body>
      <b-card-header class="border-0">
        <h3 class="mb-0 float-left">Funcionários</h3>

        <base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
          <b-icon icon="filter-square-fill" font-scale="1"></b-icon>
          <span class="btn-inner--text">Filtrar</span>
        </base-button>

        <base-button v-b-modal.modal-1 type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v:onclick="">
          <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
          <span class="btn-inner--text">Adicionar</span>
        </base-button>

      </b-card-header>

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

            <base-button v-on:click="buscarCliente" v-b-modal.modal-1 size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="eye-fill" font-scale="1"></b-icon></base-button>
            <base-button size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="pencil-fill" font-scale="1"></b-icon></base-button>
            <base-button size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="trash-fill" font-scale="1"></b-icon></base-button>

          </template>
        </el-table-column>

      </el-table>

      <b-card-footer class="py-4 d-flex justify-content-end">
        <base-pagination v-model="currentPage" :per-page="10" :total="50"></base-pagination>
      </b-card-footer>
      <b-modal id="modal-1" title="Incluir Funcionário" size="xl" @show="resetModal"
               @hidden="resetModal"
               @ok="handleOk">

        <form ref="form" @submit.stop.prevent="handleSubmit">

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
                                  v-model="Funcionario.Pessoa.Nome"
                                  :state="nameState"
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
                                  v-model="Funcionario.Pessoa.CPF"
                                  :state="cpfState"
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
                                  v-model="Funcionario.Pessoa.RG"
                                  :state="rgState"
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
                                  v-model="Funcionario.Pessoa.Email"
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
                              v-model="Funcionario.Pessoa.Telefone">
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Celular"
                              placeholder="Celular"
                              v-model="Funcionario.Pessoa.Celular">
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
                                  v-model="Funcionario.Pessoa.Cidade"
                                  :state="cidadeState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>
                <b-col lg="3">
                  <b-form-group label="UF"
                                label-for="uf-input"
                                invalid-feedback="UF é obrigatório"
                                :state="ufState">
                    <b-form-select v-model="Funcionario.Pessoa.UF"
                                   :state="ufState"
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
                                  v-model="Funcionario.Pessoa.CEP"
                                  :state="cepState"
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
                                  v-model="Funcionario.Pessoa.Endereco"
                                  :state="enderecoState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="4">
                  <base-input type="text"
                              label="Número"
                              placeholder="Número"
                              v-model="Funcionario.Pessoa.Numero">
                  </base-input>
                </b-col>

              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="Referência"
                              placeholder="Referência"
                              v-model="Funcionario.Pessoa.Referencia">
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Complemento"
                              placeholder="Complemento"
                              v-model="Funcionario.Pessoa.Complemento">
                  </base-input>
                </b-col>
              </b-row>
              <hr class="my-4">


            </div>


          </b-form-group>

        </form>

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
        Funcionario: {
          Pessoa: {
            Nome: "",
            RG: "",
            CPF: "",
            Telefone: "",
            Celular: "",
            Cidade: "",
            Email: "",
            CEP: "",
            Numero: 0,
            Complemento: "",
            Referencia: "",
            Endereco: "",
            TipoPessoa: 0,
            UF: "",
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
        this.Funcionario.Pessoa.Nome = ''
        this.Funcionario.Pessoa.CPF = ''
        this.Funcionario.Pessoa.Celular = ''
        this.Funcionario.Pessoa.CEP = ''
        this.Funcionario.Pessoa.Complemento = ''
        this.Funcionario.Pessoa.Email = ''
        this.Funcionario.Pessoa.Endereco = ''
        this.Funcionario.Pessoa.Numero = 0
        this.Funcionario.Pessoa.UF = ''
        this.Funcionario.Pessoa.Referencia = ''
        this.Funcionario.Pessoa.RG = ''
        this.Funcionario.Pessoa.Telefone = ''
        this.Funcionario.Pessoa.Cidade = ''
        this.nameState = null
        this.cidadeState = null
        this.emailState = null
        this.cpfState = null
        this.rgState = null
        this.ufState = null
        this.cepState = null
        this.enderecoState = null
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

      getFuncionarios() {
        axios.get("https://localhost:44376/Funcionario/Index", {
        }).then(response => {
          console.log(response.data)
          this.funcionarios = response.data
        })
          .catch(function (error) {
            alert(error);
          });
      },

      //getClienteById() {
      //  axios.get("https://localhost:44376/Cliente/GetClienteById", {
      //    params: this.Cliente.Pessoa
      //  }).then(response => {
      //    console.log(response.data)
      //    this.Cliente = response.data.Cliente
      //  })
      //    .catch(function (error) {
      //      alert(error);
      //    });
      //},

      sendForm() {

        axios.post("https://localhost:44376/Funcionario/Create", {
          Pessoa: this.Funcionario.Pessoa
        }).then(response => {
          if (response.data.sucess = true) {
            console.log(response.data)
            alert(response.data.description)
            //window.location.href = "#/funcionarios"
            this.$bvModal.hide("modal-1")
            this.getFuncionarios()
          }
          else {
            alert(response.data.description)
          }
        })
          .catch(function (error) {
            alert(error);
          });
        // this will be called only after form is valid. You can do api call here to login
      }

    },
    mounted() {
      this.getFuncionarios();
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

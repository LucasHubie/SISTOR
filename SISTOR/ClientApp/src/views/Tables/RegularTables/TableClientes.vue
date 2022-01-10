<template>
  <b-card style="box-shadow: 3px 0px 5px 3px #0000007d;" no-body>
    <b-card-header class="border-0">
      <h3 class="mb-0 float-left">Clientes</h3>
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
              :data="clientes">
      <el-table-column label="Nome"
                       min-width="310px"
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
                       min-width="140px">
      </el-table-column>
      <el-table-column label="Telefone"
                       prop="pessoa.telefone"
                       min-width="170px">
      </el-table-column>
      <el-table-column label="Ações"
                       min-width="170px"
                       prop="">
        <template v-slot="{row}">
          <el-dropdown trigger="click" class="dropdown">
            <base-button size="sm" type="default" style="background-color: rgb(58 99 167); margin-right: .5rem;"><b-icon icon="three-dots" font-scale="1"></b-icon></base-button>
            <el-dropdown-menu class="dropdown-menu dropdown-menu-arrow show" slot="dropdown">
              <b-dropdown-item>Gerar Ordem de Serviço</b-dropdown-item>
              <b-dropdown-item>Alterar Situação</b-dropdown-item>

            </el-dropdown-menu>
          </el-dropdown>
          <base-button v-b-modal.modal-1 size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="eye-fill" font-scale="1"></b-icon> </base-button>
          <base-button size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="pencil-fill" font-scale="1"></b-icon></base-button>
          <base-button size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="trash-fill" font-scale="1"></b-icon></base-button>

        </template>
      </el-table-column>

    </el-table>

    <b-card-footer class="py-4 d-flex justify-content-end">
      <base-pagination v-model="currentPage" :per-page="10" :total="50"></base-pagination>
    </b-card-footer>
    <b-modal id="modal-1" title="Cliente" size="xl">

      <validation-observer v-slot="{handleSubmit}" ref="formValidator">

        <b-form id="form" @submit.prevent="handleSubmit(sendForm)">
          <h6 class="heading-small text-muted mb-4">Informações do Cliente</h6>


          <div class="">
            <b-form-group>
              <b-form-radio class="custom-control-inline" v-model="selected" name="some-radios" value="F">Pessoa Fisica</b-form-radio>
              <b-form-radio class="custom-control-inline" v-model="selected" name="some-radios" value="J">Pessoa Júridica</b-form-radio>
            </b-form-group>
            <b-row>
              
              <b-col lg="6">
                <base-input type="text"
                            label="Nome"
                            placeholder="Nome"
                            required
                            v-model="Cliente.Pessoa.Nome">
                </base-input>
              </b-col>
              <b-col lg="6" v-if="selected == 'F'">
                <base-input type="text"
                            label="CPF"
                            placeholder="000.000.000-00"
                            v-model="Cliente.Pessoa.CPF">
                </base-input>
              </b-col>
              <b-col lg="6" v-if="selected == 'J'">
                <base-input type="text"
                            label="CNPJ"
                            placeholder="000.000.000-00"
                            v-model="Cliente.Pessoa.CNPJ">
                </base-input>
              </b-col>
            </b-row>
            <b-row>
              <b-col lg="6" v-if="selected == 'F'">
                <base-input type="text"
                            label="RG"
                            placeholder="RG"
                            v-model="Cliente.Pessoa.RG">
                </base-input>
              </b-col>
              <b-col lg="6" v-if="selected == 'J'">
                <base-input type="text"
                            label="Razão Social"
                            placeholder="Razão Social"
                            v-model="Cliente.Pessoa.RazaoSocial">
                </base-input>
              </b-col>
              <b-col lg="6">
                <base-input type="text"
                            label="E-mail"
                            placeholder="E-mail"
                            v-model="Cliente.Pessoa.Email">
                </base-input>
              </b-col>
            </b-row>
            <b-row>
              <b-col lg="6">
                <base-input type="text"
                            label="Telefone"
                            placeholder="Telefone"
                            v-model="Cliente.Pessoa.Telefone">
                </base-input>
              </b-col>
              <b-col lg="6">
                <base-input type="text"
                            label="Celular"
                            placeholder="Celular"
                            v-model="Cliente.Pessoa.Celular">
                </base-input>
              </b-col>
            </b-row>
            <b-row>
              <b-col lg="6">
                <base-input type="text"
                            label="Cidade"
                            placeholder="Cidade"
                            v-model="Cliente.Pessoa.Cidade">
                </base-input>
              </b-col>
              <b-col lg="3">
                <label>
                  UF
                </label>
                <b-form-select v-model="Cliente.Pessoa.UF" :options="options"></b-form-select>

              </b-col>
            </b-row>
            <b-row>
              <b-col lg="6">
                
                <base-input type="text"
                            label="CEP"
                            placeholder="CEP"
                            v-model="Cliente.Pessoa.CEP">
                </base-input>
              </b-col>
            </b-row>
            <b-row>
              <b-col lg="8">
                <base-input type="text"
                            label="Endereço"
                            placeholder="Endereço"
                            v-model="Cliente.Pessoa.Endereco">
                </base-input>
              </b-col>
              <b-col lg="4">
                <base-input type="text"
                            label="Número"
                            placeholder="Número"
                            v-model="Cliente.Pessoa.Numero">
                </base-input>
              </b-col>

            </b-row>
            <b-row>
              <b-col lg="6">
                <base-input type="text"
                            label="Referência"
                            placeholder="Referência"
                            v-model="Cliente.Pessoa.Referencia">
                </base-input>
              </b-col>
              <b-col lg="6">
                <base-input type="text"
                            label="Complemento"
                            placeholder="Complemento"
                            v-model="Cliente.Pessoa.Complemento">
                </base-input>
              </b-col>
            </b-row>
            <hr class="my-4">


          </div>


        </b-form>

      </validation-observer>

        <template #modal-footer="{ cancel }">
          <b-row>
            <b-col lg="12">
              <base-button type="success" class="float-right" style="margin-right: 10px;" v-on:click="sendForm()">
                <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
                <span class="btn-inner--text">Incluir</span>
              </base-button>

              <!--<b-button type="submit" variant="primary" class="mt-4" v:onClick="onSubmit">Criar conta</b-button>-->

              <base-button type="secondary" class="float-right" style="margin-right: 10px;" @click="cancel()">

                <span class="btn-inner--text">Cancelar</span>
              </base-button>
            </b-col>
          </b-row>
        </template>
    </b-modal>
  </b-card>

</template>
<script>
  import axios from 'axios'
  import products from '../products'
  import { Table, TableColumn, DropdownMenu, DropdownItem, Dropdown } from 'element-ui'
  export default {
    name: 'table-clientes',
    components: {
      [Table.name]: Table,
      [TableColumn.name]: TableColumn,
      [Dropdown.name]: Dropdown,
      [DropdownItem.name]: DropdownItem,
      [DropdownMenu.name]: DropdownMenu,
    },
    data() {
      return {
        clientes: [],
        products,
        uf: [],
        currentPage: 1,
        selected: 'F',
        Cliente: {
          Pessoa: {
            Nome: "",
            RG: "",
            CPF: "",
            CNPJ: "",
            RazaoSocial: "",
            NomeFantasia: "",
            Telefone: "",
            Celular: "",
            Email: "",
            CEP: "",
            Numero: 0,
            Complemento: "",
            Referencia: "",
            Endereco: "",
            TipoPessoa: 0,
          }
        },
        orcamento: {
          descricao: 'Motor WEG'
        },
        produto: {

        },
        uf: {

        },
        options: [
          { value: "AL", text: "Alagoas"},
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
          { Evalue: "RR", text: "Roraima" },
          { value: "RO", text: "Rondonia" },
          { value: "SP", text: "São Paulo" },
          { Evalue: "SC", text: "Santa Catarina" },
          { value: "SE", text: "Sergipe" },
          { value: "TO", text: "Tocantins" }
        ],
        showNovoProduto: false
      };
    },
    methods: {
      getClientes() {
        axios.get("https://localhost:44376/Cliente/Index", {
        }).then(response => {
          console.log(response.data)
          this.clientes = response.data
        })
          .catch(function (error) {
            alert(error);
          });
      },
      sendForm() {
        //if ($("#form").valid()) {
        //  alert("valido");
        //}
        //else {
        //  alert("inválido");
        //}
        axios.post("https://localhost:44376/Cliente/Create", {
          Pessoa: this.Cliente.Pessoa
        }).then(response => {
          if (response.data.sucess = true) {
            console.log(response.data)
            alert(response.data.description)
            //window.location.href = "#/funcionarios"
            this.$bvModal.hide("modal-1")
            this.getClientes()
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
      getUF() {
        axios.get("https://localhost:44376/Cliente/GetUF", {
        }).then(response => {
          console.log(response.data)
          this.uf = response.data
        })
          .catch(function (error) {
          alert(error);
        });
      }
    },
    mounted() {
      this.getClientes();
      this.getUF();
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

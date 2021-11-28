<template>
  <b-card style="box-shadow: 3px 0px 5px 3px #0000007d;" no-body>
    <b-card-header class="border-0">
      <h3 class="mb-0 float-left">Funcionário</h3>
      <base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
        <b-icon icon="filter-square-fill" font-scale="1"></b-icon>
        <span class="btn-inner--text">Filtrar</span>
      </base-button>
      <base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
        <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
        <span class="btn-inner--text">Adicionar</span>
      </base-button>

    </b-card-header>

    <el-table class="table-responsive table"
              header-row-class-name="thead-light"
              :data="funcionarios">
      <el-table-column label="Nome"
                       min-width="310px"
                       prop="name">
        <template v-slot="{row}">
          <b-media no-body class="align-items-center">
            <b-media-body>
              <span class="font-weight-600 name mb-0 text-sm">{{row.nome}}</span>
            </b-media-body>
          </b-media>
        </template>
      </el-table-column>
      <el-table-column label="E-mail"
                       prop="email"
                       min-width="140px">
      </el-table-column>
      <el-table-column label="Telefone"
                       prop="telefone"
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
    <b-modal id="modal-1" title="Funcionário" size="xl">
      <b-form @submit.prevent="updateProfile">
        <h6 class="heading-small text-muted mb-4">Informações do Funcionário</h6>
        <div class="">
          <b-form-group>
            <b-form-radio class="custom-control-inline" v-model="selected" name="some-radios" value="F">Pessoa Fisica</b-form-radio>
           
          </b-form-group>
          <b-row>
            <b-col lg="6">
              <base-input type="text"
                          label="Nome"
                          placeholder="Nome"
                          v-model="user.username">
              </base-input>
            </b-col>
            <b-col lg="6" v-if="selected == 'F'">
              <base-input type="email"
                          label="CPF"
                          placeholder="000.000.000-00"
                          v-model="user.email">
              </base-input>
            </b-col>
            <b-col lg="6" v-if="selected == 'J'">
              <base-input type="text"
                          label="CNPJ"
                          placeholder="000.000.000-00"
                          v-model="user.email">
              </base-input>
            </b-col>
          </b-row>
          <b-row v-if="showNovoCliente">
            <b-col lg="6" v-if="selected == 'F'">
              <base-input type="text"
                          label="RG"
                          placeholder="RG"
                          v-model="user.rg">
              </base-input>
            </b-col>
            <b-col lg="6" v-if="selected == 'J'">
              <base-input type="text"
                          label="Razão Social"
                          placeholder="RG"
                          v-model="user.rg">
              </base-input>
            </b-col>
            <b-col lg="6">
              <base-input type="text"
                          label="E-mail"
                          placeholder="E-mail"
                          v-model="user.email">
              </base-input>
            </b-col>
          </b-row>
          <b-row v-if="showNovoCliente">
            <b-col lg="6">
              <base-input type="text"
                          label="Telefone"
                          placeholder="Telefone"
                          v-model="user.telefone">
              </base-input>
            </b-col>
            <b-col lg="6">
              <base-input type="text"
                          label="Telefone Celular"
                          placeholder="Telefone Celular"
                          v-model="user.telefoneCel">
              </base-input>
            </b-col>
          </b-row>
          <b-row v-if="showNovoCliente">
            <b-col lg="12">
              <base-input type="text"
                          label="Endereço"
                          placeholder="Endereço"
                          v-model="user.telefoneCel">
              </base-input>
            </b-col>
          </b-row>
          <b-row v-if="showNovoCliente">
            <b-col lg="6">
              <base-input type="text"
                          label="CEP"
                          placeholder="CEP"
                          v-model="user.cep">
              </base-input>
            </b-col>
            <b-col lg="6">
              <base-input type="text"
                          label="Número"
                          placeholder="Número"
                          v-model="user.cep">
              </base-input>
            </b-col>

          </b-row>
          <b-row v-if="showNovoCliente">
            <b-col lg="6">
              <base-input type="text"
                          label="Referência"
                          placeholder="Referência"
                          v-model="user.telefoneCel">
              </base-input>
            </b-col>
            <b-col lg="6">
              <base-input type="text"
                          label="Complemento"
                          placeholder="Complemento"
                          v-model="user.cep">
              </base-input>
            </b-col>
          </b-row>
          <hr class="my-4">


        </div>


      </b-form>
      <template #modal-footer>
        <b-row v-if="showNovoCliente">
          <b-col lg="12">
            <base-button  type="success" class="float-right" style="margin-right: 10px;" v-on:click="showNovoCliente = !showNovoCliente">
              <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
              <span class="btn-inner--text">Incluir</span>
            </base-button>
            <base-button type="secondary" class="float-right" style="margin-right: 10px;" v-on:click="showNovoCliente = !showNovoCliente">

              <span class="btn-inner--text">Cancelar</span>
            </base-button>
          </b-col>
        </b-row>
      </template>
    </b-modal>
  </b-card>
 
</template>
<script>
  import funcionarios from '../funcionarios'
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
        funcionarios,
        products,
        currentPage: 1,
        selected: 'F',
        user: {
          username: ''
        },
        orcamento: {
          descricao: 'Motor WEG'
        },
        produto: {
        
        },
        showNovoCliente: true,
        showNovoProduto: false
      };
    }
  }
</script>

<style>
  input[type], input[type="*"] {
    color: black
  }

  .form-control{
    color: black !important;
  }
  </style>

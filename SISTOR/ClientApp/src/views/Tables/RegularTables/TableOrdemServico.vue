<template>
  <b-card style="box-shadow: 3px 0px 5px 3px #0000007d;" no-body>
    <b-card-header class="border-0">
      <h3 class="mb-0 float-left">Ordens de Serviço</h3>
      <base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
        <b-icon icon="filter-square-fill" font-scale="1"></b-icon>
        <span class="btn-inner--text">Filtrar</span>
      </base-button>
      <base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
        <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
        <span class="btn-inner--text">Adicionar</span>
      </base-button>
      <base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
        <b-icon icon="card-list" font-scale="1"></b-icon>
        <span class="btn-inner--text">Gerar Relatórios</span>
      </base-button>

    </b-card-header>

    <el-table class="table-responsive table"
              header-row-class-name="thead-light"
              :data="ordensServico">
      <el-table-column label="Tag ID"
                       min-width="155px"
                       prop="name">
        <template v-slot="{row}">
          <b-media no-body class="align-items-center">
            <b-media-body>
              <span class="font-weight-600 name mb-0 text-sm">{{row.orcamento.tagIdentificacao}}</span>
            </b-media-body>
          </b-media>
        </template>
      </el-table-column>
      <el-table-column label="Funcionário"
                       prop="funcionario.pessoa.nome"
                       min-width="130px">
      </el-table-column>
      <el-table-column label="Cliente"
                       prop="cliente.pessoa.nome"
                       min-width="100px">
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
      <base-pagination :per-page="10" :total="50"></base-pagination>
    </b-card-footer>
  </b-card>
 
</template>
<script>
  import axios from 'axios'
  import products from '../products'
  import { Table, TableColumn, DropdownMenu, DropdownItem, Dropdown } from 'element-ui'
  export default {
    name: 'table-ordem-servico',
    components: {
      [Table.name]: Table,
      [TableColumn.name]: TableColumn,
      [Dropdown.name]: Dropdown,
      [DropdownItem.name]: DropdownItem,
      [DropdownMenu.name]: DropdownMenu,
    },
    data() {
      return {
        ordensServico: [],
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
        showNovoCliente: false,
        showNovoProduto: false
      }
    },
    methods: {
      
      getOrdem() {
        axios.get("https://localhost:44376/Orcamento/GridOS", {
        }).then(response => {
          console.log(response.data)
          this.ordensServico = response.data
        })
          .catch(function (error) {
            alert(error);
          });
      }

    }
    , mounted() { this.getOrdem(); }
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

<template>
  <b-card style="box-shadow: 3px 0px 5px 3px #0000007d;" no-body>
    <b-card-header class="border-0">
      <h3 class="mb-0 float-left">Produto</h3>
      <base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
        <b-icon icon="filter-square-fill" font-scale="1"></b-icon>
        <span class="btn-inner--text">Filtrar</span>
      </base-button>
      <base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
        <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
        <span v-b-modal.modal-1 class="btn-inner--text">Adicionar</span>
      </base-button>

    </b-card-header>

    <el-table class="table-responsive table"
              header-row-class-name="thead-light"
              :data="produtos">
      <!--<el-table-column label="Nome"
                   min-width="310px"
                   prop="nome">
    <template v-slot="{row}">
      <b-media no-body class="align-items-center">
        <b-media-body>
          <span class="font-weight-600 name mb-0 text-sm">{{row.pessoa.nome}}</span>
        </b-media-body>
      </b-media>
    </template>
  </el-table-column>-->
      <el-table-column label="Código"
                       prop="produto.codigo"
                       min-width="140px">
      </el-table-column>
      <el-table-column label="Descrição"
                       prop="produto.descricao"
                       min-width="170px">
      </el-table-column>
      <el-table-column label="Tipo de medida"
                       prop="produto.tipoMedida"
                       min-width="170px">
      </el-table-column>
      <el-table-column label="Ações"
                       min-width="170px"
                       prop="">
        <template v-slot="{row}">
          <el-dropdown v-if="false" trigger="click" class="dropdown">
            <base-button size="sm" type="default" style="background-color: rgb(58 99 167); margin-right: .5rem;"><b-icon icon="three-dots" font-scale="1"></b-icon></base-button>

          </el-dropdown>
          <base-button size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="eye-fill" font-scale="1"></b-icon> </base-button>
          <base-button size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="pencil-fill" font-scale="1"></b-icon></base-button>
          <base-button size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="trash-fill" font-scale="1"></b-icon></base-button>

        </template>
      </el-table-column>

    </el-table>

    <b-card-footer class="py-4 d-flex justify-content-end">
      <base-pagination v-model="currentPage" :per-page="10" :total="qntdRegistros" @change="change"></base-pagination>
    </b-card-footer>
    <b-modal id="modal-1" title="Produto" size="xl">
      <b-form @submit.prevent="handleSubmit">
        <h6 class="heading-small text-muted mb-4">Informações do Produto</h6>
        <div class="">
          <b-form-group>
           
          </b-form-group>
          <b-row>
            <b-col lg="6">
              <!--<base-input type="text"
                          label="Produto"
                          placeholder="Descrição do produto"
                          :rules="{required: true}"
                          v-model="produto.descricao">
              </base-input>-->
              <b-form-group label="Produto"
                            label-for="produto-input"
                            invalid-feedback="Produto é obrigatório"
                            >
                <b-form-input id="produto-input"
                              placeholder="Descrição do produto"
                              v-model="produto.descricao"
                              required></b-form-input>
              </b-form-group>
            </b-col>

            <b-col lg="6">

              <!--<b-form-group label="Código"
                            label-for="codigo-input"
                            invalid-feedback="Código é obrigatório">
                <b-form-input id="codigo-input"
                              placeholder="Código"
                              v-model="produto.codigo"
                              required></b-form-input>
              </b-form-group>-->
              <base-input type="number"
                          label="Código"
                          placeholder="Código"
                          v-model="produto.codigo"
                          required>
              </base-input>
            </b-col>
          </b-row>
          <b-row >
            <b-col lg="6">
              <label>
                Tipos de Medida
              </label>
              <b-form-select v-model="produto.tipoMedida" :options="optionsMedidas"></b-form-select>

            </b-col>
            <b-col lg="6">
              <base-input type="number"
                          label="Valor em R$"
                          placeholder="Valor do produto"
                          v-model="produto.valor">
              </base-input>
            </b-col>
          </b-row>
          
          
          
          
          <hr class="my-4">


        </div>


      </b-form>
      <template #modal-footer="{ ok, cancel, hide }">
        <b-row >
          <b-col lg="12">
            <base-button  type="success" class="float-right" style="margin-right: 10px;">
              <span class="btn-inner--text" v-on:click="sendForm()">Confirmar</span>
            </base-button>
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
  import products from '../products'
  import { Table, TableColumn, DropdownMenu, DropdownItem, Dropdown } from 'element-ui'
  import axios from 'axios'
  export default {
    name: 'table-produtos',
    components: {
      [Table.name]: Table,
      [TableColumn.name]: TableColumn,
      [Dropdown.name]: Dropdown,
      [DropdownItem.name]: DropdownItem,
      [DropdownMenu.name]: DropdownMenu,
    },
    data() {
      return {
        produtos: [],
        products,
        currentPage: 1,
        selected: 'F',
        qntdRegistros: 0,
        produto: {
          descricao: '',
          codigo: 0,
          tipoMedida: '',
          valor: ''
        },
        optionsMedidas: [
          { value: 1, text: 'KG - Kilos' },
          { value: 2, text: 'QTD - Quantidade' },
          { value: 3, text: 'M - Metros' },
          { value: 4, text: 'CM - Centímetros' },
          { value: 5, text: 'MM - Milímetros' },
          { value: 6, text: 'L - Litros' }
        ],
        
      }
    },
    methods: {
      sendForm() {
        console.log(this.produtos)
        axios.post("https://localhost:44376/Produto/Create", {
          params: this.produtos
        }).then(response => {
          if (response.data.sucess == true) {
            console.log(response.data)
            alert(response.data.description)
            //window.location.href = "#/funcionarios"
            this.$bvModal.hide("modal-1")
            this.getProdutos(1, 10)
          }
          else {
            alert(response.data.description)
          }

        })
          .catch(function (error) {
            alert(error);
          });
      },
      getProdutos(pageN, pageS) {
        axios.get("https://localhost:44376/Produto/Index", {
          params: { "pageNumber": pageN, "pageSize": pageS }
        }).then(response => {
          console.log(response.data)
          this.produtos = response.data.lst;
          this.qntdRegistros = response.data.qntdRegistros;
        })
          .catch(function (error) {
            alert("Falha ao Carregar Produtos");
          });
      },
      
      change(val) {
        this.getProdutos(val, 10);
      },
    }
    , mounted() { this.getProdutos(1, 10); }
  };
</script>

<style>
  input[type], input[type="*"] {
    color: black
  }

  .form-control{
    color: black !important;
  }
  </style>

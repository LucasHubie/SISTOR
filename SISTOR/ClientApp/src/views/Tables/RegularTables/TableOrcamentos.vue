<template>
  <b-card style="box-shadow: 3px 0px 5px 3px #0000007d;" no-body>
    <b-card-header class="border-0">
      <h3 class="mb-0 float-left">Orçamentos</h3>
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
              :data="projects">
      <el-table-column label="Nome"
                       min-width="310px"
                       prop="name">
        <template v-slot="{row}">
          <b-media no-body class="align-items-center">
            <b-media-body>
              <span class="font-weight-600 name mb-0 text-sm">{{row.title}}</span>
            </b-media-body>
          </b-media>
        </template>
      </el-table-column>
      <el-table-column label="Cliente"
                       prop="budget"
                       min-width="140px">
      </el-table-column>

      <el-table-column label="Status"
                       min-width="170px"
                       prop="status">
        <template v-slot="{row}">
          <badge class="badge-dot mr-4" type="">
            <i :class="`bg-${row.statusType}`"></i>
            <span class="status" :class="`text-${row.statusType}`">{{row.status}}</span>
          </badge>
        </template>
      </el-table-column>
      <el-table-column label="Ações"
                       min-width="170px"
                       prop="">
        <template v-slot="{row}">
          <el-dropdown trigger="click" class="dropdown">
            <base-button size="sm" type="default" style="background-color: rgb(58 99 167); margin-right: .5rem;"><b-icon icon="three-dots" font-scale="1"></b-icon></base-button>
            <el-dropdown-menu class="dropdown-menu dropdown-menu-arrow show" slot="dropdown">
              <b-dropdown-item v-b-modal.modal-2>Gerar Ordem de Serviço</b-dropdown-item>
              <b-dropdown-item v-b-modal.modal-3>Alterar Situação</b-dropdown-item>
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
    <b-modal id="modal-1" title="Orçamento" size="xl">
      <b-form @submit.prevent="updateProfile">
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
          <b-row v-if="user.username != ''">
            <b-col lg="6">
              <small>
                Cliente não encontrado
              </small>
            </b-col>
            <b-col lg="6">
              <base-button size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v-on:click="showNovoCliente = !showNovoCliente">
                <b-icon icon="person-plus-fill" font-scale="1"></b-icon>
                <span class="btn-inner--text">Novo Cliente</span>
              </base-button>
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
          <b-row v-if="showNovoCliente">
            <b-col lg="12">
              <base-button size="sm" type="success" class="float-right" style="margin-right: 10px;" v-on:click="showNovoCliente = !showNovoCliente">
                <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
                <span class="btn-inner--text">Incluir</span>
              </base-button>
              <base-button size="sm" type="secondary" class="float-right" style="margin-right: 10px;" v-on:click="showNovoCliente = !showNovoCliente">

                <span class="btn-inner--text">Cancelar</span>
              </base-button>
            </b-col>
          </b-row>
        </div>
        <hr class="my-4">
        <h6 class="heading-small text-muted mb-4">Descrição Orçamento</h6>
        <b-row>
          <b-col lg="12">
            <base-input type="text"
                        label="Descrição"
                        placeholder="Descrição"
                        v-model="orcamento.descricao">
            </base-input>
          </b-col>
        </b-row>
        <hr class="my-4">

        <!-- Address -->
        <!-- Address -->
        <h6 class="heading-small text-muted mb-4">Produtos</h6>

        <div class="">
          <b-row>
            <b-col lg="12">
              <base-button size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px; margin-bottom: 10px;" v-on:click="showNovoProduto = !showNovoProduto">
                <b-icon icon="clipboard-plus" font-scale="1"></b-icon>
                <span class="btn-inner--text">Adicionar Produto</span>
              </base-button>
            </b-col>
          </b-row>
          <b-row v-if="showNovoProduto">
            <b-col lg="6">
              <base-input type="text"
                          label="Descrição"
                          placeholder="Descrição"
                          v-model="produto.ds">
              </base-input>
            </b-col>
            <b-col lg="3">
              <base-input type="text"
                          label="Quantidade"
                          placeholder="Quantidade"
                          v-model="produto.qt">
              </base-input>
            </b-col>
            <b-col lg="3">
              <base-input type="text"
                          label="Valor"
                          placeholder="Valor"
                          v-model="produto.vl">
              </base-input>
            </b-col>
          </b-row>
          <b-row>
            <el-table class="table-responsive table"
                      header-row-class-name="thead-light"
                      :data="products">
              <el-table-column label="Descrição"
                               min-width="310px"
                               prop="name">
                <template v-slot="{row}">
                  <b-media no-body class="align-items-center">
                    <b-media-body>
                      <span class="font-weight-600 name mb-0 text-sm">{{row.descricao}}</span>
                    </b-media-body>
                  </b-media>
                </template>
              </el-table-column>
              <el-table-column label="Qntd"
                               min-width="50px"
                               prop="name">
                <template v-slot="{row}">
                  <b-media no-body class="align-items-center">
                    <b-media-body>
                      <span class="font-weight-600 name mb-0 text-sm">{{row.quantidade}}</span>
                    </b-media-body>
                  </b-media>
                </template>
              </el-table-column>
              <el-table-column label="Valor Unitario"
                               min-width="310px"
                               prop="name">
                <template v-slot="{row}">
                  <b-media no-body class="align-items-center">
                    <b-media-body>
                      <span class="font-weight-600 name mb-0 text-sm">R${{row.valor}}</span>
                    </b-media-body>
                  </b-media>
                </template>
              </el-table-column>
              <el-table-column label="Valor Total"
                               min-width="310px"
                               prop="name">
                <template v-slot="{row}">
                  <b-media no-body class="align-items-center">
                    <b-media-body>
                      <span class="font-weight-600 name mb-0 text-sm">R${{row.valor}}</span>
                    </b-media-body>
                  </b-media>
                </template>
              </el-table-column>
            </el-table>
          </b-row>

        </div>

        <hr class="my-4">
        <!-- Description -->
        <h6 class="heading-small text-muted mb-4">Finalização Orçamento</h6>
        <b-row>
          <b-col lg="6">
            <base-input type="text"
                        label="Valor Mão de Obra"
                        placeholder="Valor Mão de Obra"
                        v-model="orcamento.valor">
            </base-input>
          </b-col>
          <b-col lg="3">
            <base-input type="text"
                        label="Situação"
                        placeholder="Quantidade"
                        v-model="orcamento.st">
              <select class="form-control">
                <option>Aguardando Aprovação</option>
                <option>Aprovado</option>
              </select>
            </base-input>
          </b-col>
          <b-col lg="3">
            <base-input type="text"
                        label="Valor Total"
                        placeholder="Valor"
                        v-model="orcamento.valorTotal">
            </base-input>
          </b-col>
        </b-row>

      </b-form>
      <template #modal-footer>
        <b-row v-if="showNovoCliente">
          <b-col lg="12">
            <base-button type="success" class="float-right" style="margin-right: 10px;">
              <span class="btn-inner--text">Confirmar</span>
            </base-button>
            <base-button type="secondary" class="float-right" style="margin-right: 10px;">

              <span class="btn-inner--text">Cancelar</span>
            </base-button>
          </b-col>
        </b-row>
      </template>
    </b-modal>
    <b-modal id="modal-2" title="Ordem Serviço" size="xl">
      <b-form @submit.prevent="updateProfile">
        <h6 class="heading-small text-muted mb-4">Informações da Ordem Serviço</h6>
        <div class="">
          <b-row>
            <b-col lg="12">
              <base-input type="text"
                          label="Nome"
                          placeholder="Nome"
                          v-model="orcamento.desc">
              </base-input>
            </b-col>
          </b-row>
        </div>


        <b-row>
          <b-col lg="6">
            <base-input type="text"
                        label="Funcionário"
                        placeholder="Funcionário"
                        v-model="orcamento.func">
              <select class="form-control">
                <option>Lucas Noga</option>
                <option>Rafael Souza</option>
              </select>
            </base-input>
          </b-col>
          <b-col lg="6">
            <base-input type="text"
                        label="Data Prevista Entrega"
                        placeholder="00/00/0000"
                        v-model="orcamento.dtFin">
            </base-input>
          </b-col>
        </b-row>
        <b-row>
          <div class="">
            <b-form-group label="Descrição Adicional" label-class="form-control-label" class="mb-0" label-for="about-form-textaria">
              <!--  <label class="form-control-label">About Me</label> -->
              <b-form-textarea rows="4" value="Motor deve ser testado antes de realizar a entrega" id="about-form-textaria" placeholder="A few words about you ..."></b-form-textarea>
            </b-form-group>
          </div>
        </b-row>
      </b-form>
      <template #modal-footer>
        <b-row v-if="showNovoCliente">
          <b-col lg="12">
            <base-button type="success" class="float-right" style="margin-right: 10px;">
              <span class="btn-inner--text">Confirmar</span>
            </base-button>
            <base-button type="secondary" class="float-right" style="margin-right: 10px;">

              <span class="btn-inner--text">Cancelar</span>
            </base-button>
          </b-col>
        </b-row>
      </template>
    </b-modal>
    <b-modal id="modal-3" title="Orçamento - Situação" size="xl">
      <b-form @submit.prevent="updateProfile">
        <h6 class="heading-small text-muted mb-4">Alterar</h6>
        <div class="">
          <b-row>
            <b-col lg="6">
              <base-input type="text"
                          label="Nome"
                          placeholder="Nome"
                          v-model="orcamento.desc">
              </base-input>
            </b-col>
            <b-col lg="6">
              <base-input type="text"
                          label="Situação"
                          placeholder="Funcionário"
                          v-model="orcamento.func">
                <select class="form-control">
                  <option>Aprovado</option>
                  <option>Finalizado</option>
                  <option>Cancelado</option>
                  <option>Em Andamento</option>
                </select>
              </base-input>
            </b-col>
          </b-row>
        </div>

      </b-form>
      <template #modal-footer>
        <b-row v-if="showNovoCliente">
          <b-col lg="12">
            <base-button type="success" class="float-right" style="margin-right: 10px;">
              <span class="btn-inner--text">Confirmar</span>
            </base-button>
            <base-button type="secondary" class="float-right" style="margin-right: 10px;">

              <span class="btn-inner--text">Cancelar</span>
            </base-button>
          </b-col>
        </b-row>
      </template>
    </b-modal>
  </b-card>
 
</template>
<script>
  import projects from '../projects'
  import products from '../products'
  import { Table, TableColumn, DropdownMenu, DropdownItem, Dropdown } from 'element-ui'
  export default {
    name: 'table-orcamentos',
    components: {
      [Table.name]: Table,
      [TableColumn.name]: TableColumn,
      [Dropdown.name]: Dropdown,
      [DropdownItem.name]: DropdownItem,
      [DropdownMenu.name]: DropdownMenu,
    },
    data() {
      return {
        projects,
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

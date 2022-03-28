<template>
  <div>
    <b-card style="box-shadow: 3px 0px 5px 3px #0000007d;" no-body>
      <b-card-header class="border-0">
        <h3 class="mb-0 float-left">Ordens de Serviço</h3>
        <base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
          <b-icon icon="filter-square-fill" font-scale="1"></b-icon>
          <span class="btn-inner--text">Filtrar</span>
        </base-button>
        <base-button v-on:click="resetModal" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
          <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
          <span v-b-modal.modal-1 class="btn-inner--text">Adicionar</span>
        </base-button>
        <!--<base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
      <b-icon icon="card-list" font-scale="1"></b-icon>
      <span class="btn-inner--text">Gerar Relatórios</span>
    </base-button>-->

      </b-card-header>

      <el-table class="table-responsive table"
                header-row-class-name="thead-light"
                :data="ordensServico">
        <el-table-column label="Tag Identificação"
                         min-width="90px"
                         prop="name">
          <template v-slot="{row}">
            <b-media no-body class="align-items-center">
              <b-media-body>
                <span class="font-weight-600 name mb-0 text-sm">{{row.orcamento.tagIdentificacao}}</span>
              </b-media-body>
            </b-media>
          </template>
        </el-table-column>
        <!--<el-table-column label="Produtos"
                         min-width="90px"
                         prop="name">
          <template v-slot="{row}">
            <b-media no-body class="align-items-center">
              <b-media-body>
                <span class="font-weight-600 name mb-0 text-sm">{{row.itensOrcamento.produto.descricao}}</span>
              </b-media-body>
            </b-media>
          </template>
        </el-table-column>-->
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
                <b-dropdown-item v-on:click="showModalOS(row.id, row.cliente.id, row.tagIdentificacao)">Fechar Ordem de Serviço</b-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown>
            <base-button v-on:click="GetOrdemServicoById(row.id, 'Visualizar')" v-b-modal.modal-2 size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="eye-fill" font-scale="1"></b-icon> </base-button>
            <base-button v-on:click="GetOrdemServicoById(row.id, 'Alterar')" size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="pencil-fill" font-scale="1"></b-icon></base-button>
            <base-button size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="trash-fill" font-scale="1"></b-icon></base-button>

          </template>
        </el-table-column>

      </el-table>

      <b-card-footer class="py-4 d-flex justify-content-end">
        <base-pagination :per-page="10" :total="qntdRegistros"></base-pagination>
      </b-card-footer>

      <!--modal inclusao e edição-->
      <b-modal id="modal-1" title="Ordem de serviço" size="lg">
        <b-form @submit.prevent="handleSubmit">
          <h6 class="heading-small text-muted mb-4">Informações da Ordem de Serviço</h6>
          <div class="">
            <b-form-group>

            </b-form-group>
            <b-row>
              <div class="">
                <b-form-group label="Observações da  Ordem de Serviço" label-class="form-control-label" class="mb-0" label-for="about-form-textaria">
                  <!--  <label class="form-control-label">About Me</label> -->
                  <b-form-textarea rows="4" v-model="ordemServico.observacoes" id="about-form-textaria" placeholder="Descrição da Ordem de Serviço"></b-form-textarea>
                </b-form-group>
              </div>
            </b-row>
            <b-row>
              <b-col lg="6">
                <label>
                  Funcionário
                </label>
                <b-form-select v-model="ordemServico.idFuncionario" :options="funcionarios"></b-form-select>
              </b-col>
              <b-col lg="6">
                <label>
                  Cliente
                </label>
                <b-form-select v-model="ordemServico.idCliente" :options="clientes"></b-form-select>
              </b-col>
            </b-row>

            <hr class="my-4">
            <!--<b-col lg="6">
    <label>
      Produtos
    </label>
    <b-form-select v-model="ordemServico.idProduto" :options="produtos"></b-form-select>
  </b-col>
  <b-col lg="6">
    <base-input type="number"
                label="Valor do produto"
                placeholder="Valor do produto"
                :disabled="disable"
                v-model="produto.valor">
    </base-input>
  </b-col>-->
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
                <b-col lg="3">
                  <base-input type="text"
                              label="Informe o Código/Descrição"
                              placeholder="Código/Descrição"
                              v-model="buscaproduto">
                  </base-input>
                </b-col>
              </b-row>
              <b-row>
                <div v-if="produtos.length > 0" class="container">
                  <table class="table table-striped table-bordered">
                    <tbody>
                      <tr v-for="produtoloop in produtos" style=" background-color: white;" :key="produtoloop.id">
                        <td style="vertical-align: middle; " class="tdpading05">{{produtoloop.codigo}}</td>
                        <td style="vertical-align: middle; ">{{produtoloop.descricao}}</td>
                        <td style="vertical-align: middle; ">R${{produtoloop.valor}}</td>
                        <td style="text-align: right; width: 10%">
                          <input type="number"
                                 label="Quantidade"
                                 placeholder="Quantidade"
                                 v-model="produtoloop.quantidade">
                        </td>
                        <td style="width:10%">
                          <base-button size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v-on:click="addProduto(produtoloop)">
                            <span class="btn-inner--text">Adicionar</span>
                          </base-button>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </div>
              </b-row>
              <b-row>
                <el-table class="table-responsive table"
                          header-row-class-name="thead-light"
                          :data="lstprodutos">
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
                          <span class="font-weight-600 name mb-0 text-sm">R${{row.valorItem}}</span>
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
                          <span class="font-weight-600 name mb-0 text-sm">R${{row.valorItem * row.quantidade }}</span>
                        </b-media-body>
                      </b-media>
                    </template>
                  </el-table-column>
                  <template slot="empty">
                    Sem Registros
                  </template>
                </el-table>
              </b-row>

            </div>

            <hr class="my-4">

            <b-row>
              <b-col lg="6">
                <base-input type="number"
                            label="Valor da mão de obra"
                            placeholder="Valor da mão de obra"
                            v-model="ordemServico.maoDeObra">
                </base-input>
              </b-col>
              <b-col lg="6">
                <base-input type="number"
                            label="Valor Total"
                            placeholder="Valor "
                            v-model="ordemServico.valorTotal">
                </base-input>
              </b-col>
            </b-row>
            <hr class="my-4">
          </div>
        </b-form>
        <template #modal-footer="{ ok, cancel, hide }">
          <b-row>
            <b-col lg="12">
              <base-button type="success" class="float-right" style="margin-right: 10px;">
                <span class="btn-inner--text" v-on:click="sendForm()">Confirmar</span>
              </base-button>
              <base-button type="secondary" class="float-right" style="margin-right: 10px;" @click="cancel()">

                <span class="btn-inner--text">Cancelar</span>
              </base-button>
            </b-col>
          </b-row>
        </template>
      </b-modal>

      <!--modal details-->
      <b-modal id="modal-2" title="Ordem de serviço" size="lg">
        <b-form @submit.prevent="handleSubmit">
          <h6 class="heading-small text-muted mb-4">Informações da Ordem de Serviço</h6>
          <div class="">
            <b-form-group>

            </b-form-group>
            <b-row>
              <div class="">
                <b-form-group label="Observações da  Ordem de Serviço" label-class="form-control-label" class="mb-0" label-for="about-form-textaria">
                  <!--  <label class="form-control-label">About Me</label> -->
                  <b-form-textarea rows="4" v-model="ordemServico.observacoes" id="about-form-textaria" placeholder="Descrição da Ordem de Serviço" :disabled="disable"></b-form-textarea>
                </b-form-group>
              </div>
            </b-row>
            <b-row>
              <b-col lg="6">
                <label>
                  Funcionário
                </label>
                <b-form-select v-model="ordemServico.idFuncionario" :options="funcionarios" :disabled="disable"></b-form-select>
              </b-col>
              <b-col lg="6">
                <label>
                  Cliente
                </label>
                <b-form-select v-model="ordemServico.idCliente" :options="clientes" :disabled="disable"></b-form-select>
              </b-col>
            </b-row>
            <b-row>
              <b-col lg="6">
                <label>
                  Produtos
                </label>
                <b-form-select v-model="ordemServico.idProduto" :options="itensOrcamento" :disabled="disable"></b-form-select>
              </b-col>
              <b-col lg="6">
                <base-input type="number"
                            label="Valor do produto"
                            placeholder="Valor do produto"
                            :disabled="disable"
                            v-model="produto.valor">
                </base-input>
              </b-col>
            </b-row>
            <b-row>
              <b-col lg="6">
                <base-input type="number"
                            label="Valor da mão de obra"
                            placeholder="Valor da mão de obra"
                            :disabled="disable"
                            v-model="ordemServico.maoDeObra">
                </base-input>
              </b-col>
              <b-col lg="6">
                <base-input type="number"
                            label="Valor Total"
                            placeholder="Valor "
                            :disabled="disable"
                            v-model="ordemServico.valorTotal">
                </base-input>
              </b-col>
            </b-row>
            <hr class="my-4">
          </div>
        </b-form>
        <template #modal-footer="{ ok, cancel, hide }">
          <b-row>
            <b-col lg="12">
              <base-button type="success" class="float-right" style="margin-right: 10px;">
                <span class="btn-inner--text" v-on:click="cancel()">Fechar</span>
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
        itensOrcamento: [],
        products,
        currentPage: 1,
        selected: 'F',
        qntdRegistros: 0,
        disable: false,
        tpOperacao: 'Incluir',
        buscaproduto: '',
        ordemServico: {
          dataInclusao: '',
          horasTrabalhadas: '',
          observacoes: '',
          maoDeObra: 0,
          valorTotal: 0,
          situacao: '',
          idFuncionario: 0,
          idProduto: 0,
          idCliente: 0
        },
        funcionarios: [],
        produtos: [],
        clientes: [],
        lstprodutos: [],
        orcamento: {
          descricao: 'Motor WEG'
        },
        produto: {
          valor: 0
        },
        itemOrcamento: {

        },
        showNovoCliente: false,
        showNovoProduto: false
      }
    },
    methods: {

      resetModal() {
        this.ordemServico.observacoes = ''
        this.ordemServico.maoDeObra = 0
        this.ordemServico.dataInclusao = ''
        this.ordemServico.valorTotal = 0
        this.ordemServico.situacao = ''
        this.ordemServico.idFuncionario = 0
        this.ordemServico.idProduto = 0
        this.ordemServico.idCliente = 0
        this.produto.valor = 0
        this.tpOperacao = 'Incluir'
        this.disable = true
      },
      getOrdemServico() {
        axios.get("https://localhost:44376/OrdemServico/GetOrdemServico", {
        }).then(response => {
          console.log(response.data)
          this.ordensServico = response.data
        })
          .catch(function (error) {
            alert(error);
          });
      },

      GetItensOrcamento() {
        axios.get("https://localhost:44376/Orcamento/GetItensOrcamento", {
        }).then(response => {
          console.log(response.data)
          this.itensOrcamento = response.data
        })
          .catch(function (error) {
            alert(error);
          });
      },

      GetItensOrcamentoById(id) {
        axios.get("https://localhost:44376/Orcamento/GetItensOrcamentoById", {
          params: { "id": id }
        }).then(response => {
          console.log(response.data)
          this.itensOrcamento = response.data
        })
          .catch(function (error) {
            alert(error);
          });
      },

      GetOrdemServicoById(id, tpOperacao) {
        if (tpOperacao == "Visualizar") {
          this.GetItensOrcamentoById(id)
          this.disable = true
        }
        axios.get("https://localhost:44376/OrdemServico/GetOrdemServicoById", {
          params: { "id": id }
        }).then(response => {
          console.log(response.data)
          this.ordemServico = response.data.ordemServico
          console.log(this.ordemServico)

          if (tpOperacao == "Alterar") {
            this.$bvModal.show("modal-1");
            this.tpOperacao = tpOperacao
            this.disable = true
          }
        })
          .catch(function (error) {
            alert(error);
          });
      },

      addProduto(produto) {
        console.log(produto);
        produto.valorItem = produto.valor;
        produto.valortotal = (produto.valor * produto.quantidade);
        this.lstprodutos.push(produto);
        //  this.produto = {};
        // this.showNovoProduto = false;
      },

      getFuncionarios() {
        axios.get("https://localhost:44376/Funcionario/GetFuncionario", {
        }).then(response => {
          console.log('func', response.data);
          for (var i = 0; i < response.data.length; i++) {
            var opt = { value: response.data[i].id, text: response.data[i].pessoa.nome };
            this.funcionarios.push(opt);
          }
          console.log('lstfunc', this.funcionarios);

        })
          .catch(function (error) {
            alert("Falha ao Carregar Funcionarios");
          });
      },

      //getProdutos() {
      //  axios.get("https://localhost:44376/Produto/GetProduto", {
      //  }).then(response => {
      //    console.log('func', response.data);
      //    for (var i = 0; i < response.data.length; i++) {
      //      var opt = { value: response.data[i].id, text: response.data[i].descricao };
      //      this.produtos.push(opt);
      //    }
      //    console.log('lstprodutos', this.produtos);

      //  })
      //    .catch(function (error) {
      //      alert("Falha ao Carregar Funcionarios");
      //    });
      //},

      getProdutos(busca, pageN, pageS) {
        axios.get("https://localhost:44376/Produto/Index", {
          params: { "busca": busca, "pageNumber": pageN, "pageSize": pageS }
        }).then(response => {
          console.log(response.data)
          this.produtos = response.data.lst;
          // this.qntdRegistros = response.data.qntdRegistros;
        })
          .catch(function (error) {
            // alert("Falha ao Carregar Produtos");
          });
      },

      getClientes() {
        axios.get("https://localhost:44376/Cliente/GetCliente", {
        }).then(response => {
          console.log('func', response.data);
          for (var i = 0; i < response.data.length; i++) {
            var opt = { value: response.data[i].id, text: response.data[i].pessoa.nome + response.data[i].pessoa.nomeFantasia };
            this.clientes.push(opt);
          }
          console.log('lstclientes', this.clientes);

        })
          .catch(function (error) {
            alert("Falha ao Carregar Clientes");
          });
      },

    }
    , mounted()
    {
      this.getOrdemServico();
      this.getFuncionarios();
      //this.getProdutos();
      this.GetItensOrcamento();
      this.getClientes();
    },
    watch: {
      currentPage: function (novo, velho) {
      },

      buscaproduto: function (novo, velho) {
        if (novo == '') {
          this.produtos = [];
        }
        else {
          this.getProdutos(novo, 1, 5)
        }
      },
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

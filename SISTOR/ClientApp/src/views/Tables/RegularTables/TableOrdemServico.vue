<template>
  <div>
    <b-alert :show="dismissCountDown" dismissible @dismissed="dismissCountDown=0" @dismiss-count-down="countDownChanged" :variant="variant">
      {{txtAlert}}
    </b-alert>
    <b-card style="box-shadow: 3px 0px 5px 3px #0000007d;" no-body>
      <b-card-header class="border-0">
        <h3 class="mb-0 float-left">Ordens de Serviço</h3>
        <base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
          <b-icon icon="filter-square-fill" font-scale="1"></b-icon>
          <span class="btn-inner--text">Filtrar</span>
        </base-button>
        <base-button v-b-modal.modal-1 v-on:click="resetModal" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
          <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
          <span  class="btn-inner--text">Adicionar</span>
        </base-button>
        <!--<base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
        <b-icon icon="card-list" font-scale="1"></b-icon>
        <span class="btn-inner--text">Gerar Relatórios</span>
      </base-button>-->

      </b-card-header>

      <el-table class="table-responsive table"
                header-row-class-name="thead-light"
                :data="ordensServico">
        <el-table-column label="Nº Ordem"
                         min-width="120px"
                         prop="name">
          <template v-slot="{row}">
            <b-media no-body class="align-items-center">
              <b-media-body>
                <span class="font-weight-600 name mb-0 text-sm">{{row.id}}</span>
              </b-media-body>
            </b-media>
          </template>
        </el-table-column>
        <el-table-column label="Tag Identificação"
                         min-width="160px"
                         prop="name">
          <template v-slot="{row}">
            <b-media no-body class="align-items-center">
              <b-media-body v-if="row.idOrcamento != null && row.idOrcamento != '' ">
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
                         min-width="160px">
        </el-table-column>
        <el-table-column label="Cliente"
                         prop="cliente.pessoa.nome"
                         min-width="160px">
        </el-table-column>

        <el-table-column label="Ações"
                         min-width="170px"
                         prop="">
          <template v-slot="{row}">
            <!--<el-dropdown trigger="click" class="dropdown">
        <base-button size="sm" type="default" style="background-color: rgb(58 99 167); margin-right: .5rem;"><b-icon icon="three-dots" font-scale="1"></b-icon></base-button>
        <el-dropdown-menu class="dropdown-menu dropdown-menu-arrow show" slot="dropdown">
          <b-dropdown-item v-on:click="showModalOS(row.id, row.cliente.id, row.tagIdentificacao)">Fechar Ordem de Serviço</b-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>-->
            <base-button v-on:click="GetOrdemServicoById(row.id, 'Visualizar')" v-b-modal.modal-2 size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="eye-fill" font-scale="1"></b-icon> </base-button>
            <base-button v-on:click="GetOrdemServicoById(row.id, 'Editar')" size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="pencil-fill" font-scale="1"></b-icon></base-button>
            <base-button v-on:click="deleteOrdemServicoConfirmed(row.id)" size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="trash-fill" font-scale="1"></b-icon></base-button>

          </template>
        </el-table-column>

      </el-table>

      <b-card-footer class="py-4 d-flex justify-content-end">
        <base-pagination :per-page="10" :total="qntdRegistros"></base-pagination>
      </b-card-footer>

      <!--modal inclusao e edição-->
      <b-modal id="modal-1" title="Ordem de serviço" size="lg">
        <validation-observer v-slot="{handleSubmit}" ref="formValidatorOrdemServico">
          <b-form id="formOrdemServico" @submit.prevent="handleSubmit(sendForm)">
            <h6 class="heading-small text-muted mb-4">Informações da Ordem de Serviço</h6>
            <div class="">
              <b-row>
                <b-col lg="6">
                  <label>
                    Funcionário
                  </label>
                  <b-form-select :disabled="disabledall" v-model="ordemServico.idFuncionario" :options="funcionarios" required></b-form-select>
                </b-col>
                <b-col lg="6">
                  <label>
                    Cliente
                  </label>
                  <b-form-select :disabled="disabledall" v-model="ordemServico.idCliente" :options="clientes" required></b-form-select>
                </b-col>
              </b-row>
              <b-row>
                <div class="">
                  <b-form-group label="Observações da  Ordem de Serviço" label-class="form-control-label" class="mb-0" label-for="about-form-textaria">
                    <!--  <label class="form-control-label">About Me</label> -->
                    <b-form-textarea :disabled="disabledall" rows="4" v-model="ordemServico.observacoes" id="about-form-textaria" placeholder="Descrição da Ordem de Serviço" required></b-form-textarea>
                  </b-form-group>
                </div>
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
                <!--<b-row>
      <b-col lg="12">
        <base-button size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px; margin-bottom: 10px;" v-on:click="showNovoProduto = !showNovoProduto">
          <b-icon icon="clipboard-plus" font-scale="1"></b-icon>
          <span class="btn-inner--text">Adicionar Produto</span>
        </base-button>
      </b-col>
    </b-row>-->
                <b-row v-if="tpOperacao != 'Visualizar'">
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
                      <thead>
                        <tr>
                          <th>Código</th>
                          <th>Descrição</th>
                          <th>Valor</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr v-for="produtoloop in produtos" style=" background-color: white;" :key="produtoloop.id">
                          <td style="vertical-align: middle; " class="tdpading05">{{produtoloop.codigo}}</td>
                          <td style="vertical-align: middle; " class="tdpading05">{{produtoloop.descricao}}</td>
                          <td style="vertical-align: middle; " class="tdpading05">R$ {{produtoloop.valor}}</td>
                          <td style="text-align: right; width: 10%" class="tdpading05">
                            <input type="number"
                                   label="Quantidade"
                                   placeholder="Quantidade"
                                   v-model="produtoloop.qntd">
                          </td>
                          <td style="width:10%" class="tdpading05">
                            <base-button :disabled="produtoloop.qntd == null || produtoloop.qntd == '' || produtoloop.qntd == 0" size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v-on:click="addProduto(produtoloop)">
                              <span class="btn-inner--text">Adicionar</span>
                            </base-button>
                          </td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                  <h5 v-if="buscaproduto != '' && produtos.length == 0">Nenhum produto encontrado</h5>
                </b-row>
                <b-row>
                  <div v-if="lstprodutos.length > 0" class="container">
                    <h4>Itens do Orçamento</h4>
                    <table class="table">
                      <thead>
                        <tr>
                          <th>Descrição</th>
                          <th>Valor Unitário</th>
                          <th>Qntd</th>
                          <th>Valor Total</th>
                          <th></th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr v-for="(produtoloop, index) in lstprodutos" style=" background-color: white;" :key="produtoloop.id">
                          <td style="vertical-align: middle; " class="tdpading05">{{produtoloop.descricao}}</td>
                          <td style="vertical-align: middle; " class="tdpading05">R$ {{produtoloop.valorItem}}</td>
                          <td style="vertical-align: middle; " class="tdpading05">{{produtoloop.quantidade}}</td>
                          <td style="vertical-align: middle; " class="tdpading05">R$ {{produtoloop.valorItem * produtoloop.quantidade}}</td>
                          <td style="width:10%" class="tdpading05">
                            <base-button v-if="tpOperacao != 'Visualizar'" size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v-on:click="removeProduto(index)">
                              <span class="btn-inner--text">Remover</span>
                            </base-button>
                          </td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                </b-row>

              </div>


              <hr class="my-4">

              <b-row>
                <b-col lg="6">
                  <base-input type="number"
                              label="Valor da mão de obra"
                              placeholder="Valor da mão de obra"
                              v-model="ordemServico.maoDeObra"
                              name="Valor da mão de obra"
                              required
                              :disabled="disabledall">
                  </base-input>
                </b-col>
              </b-row>
              <b-row>
                <b-col lg="4">
                  Valor Total Ordem de Serviço: R$ {{ Sum  }}
                </b-col>
              </b-row>
              <hr class="my-4">
            </div>
            <b-alert :show="dismissCountDown" dismissible @dismissed="dismissCountDown=0" @dismiss-count-down="countDownChanged" :variant="variant">
              {{txtAlert}}
            </b-alert>
          </b-form>
          </validation-observer>
        <template #modal-footer="{ ok, cancel, hide }">
          <b-row v-if="tpOperacao != 'Visualizar'">
            <b-col lg="12">
              <base-button type="success" class="float-right" style="margin-right: 10px;" v-on:click="sendForm()">
                <span class="btn-inner--text">Confirmar</span>
              </base-button>
              <base-button type="secondary" class="float-right" style="margin-right: 10px;" @click="cancel()">

                <span class="btn-inner--text">Cancelar</span>
              </base-button>
            </b-col>
          </b-row>
          <b-row v-else>
            <b-col lg="12">
              <base-button type="success" class="float-right" style="margin-right: 10px;" @click="cancel()">
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
        disabledall: false,
        nomeclientebusca: '',
        buscaproduto: '',
        dismissSecs: 5,
        dismissCountDown: 0,
        variant: "info",
        txtAlert: "",
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
          maoDeObra: '',
          valorTotal: '',
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
      async sendForm() {
        let $this = this;
        const isValid = await this.$refs.formValidatorOrdemServico.validate();
        console.log(this.ordemServico, 'dsad')
        if (this.ordemServico.idFuncionario == null) {
          $this.showAlert("Informe o Funcionario", "warning");
          return;
        } if (this.ordemServico.idCliente == null) {
          $this.showAlert("Informe o Cliente", "warning");
          return;
        }
        if (this.ordemServico.observacoes == null) {
          $this.showAlert("Informe a Descrição", "warning");
          return;
        }
        if (isValid) {
          console.log(this.tpOperacao);
          var url = "https://localhost:44376/OrdemServico/CriarOrdemServico";
          if (this.tpOperacao == "Editar") {
            url = "https://localhost:44376/OrdemServico/EditarOrdemServico";
          }
          axios.post(url, {
            ordemServico: this.ordemServico, lstprodutos: this.lstprodutos
          }).then(response => {
            if (response.data.sucess == true) {
              //  console.log(response.data)
              $this.showAlert(response.data.description, "success");
              //window.location.href = "#/funcionarios"
              this.$bvModal.hide("modal-1")
              this.getOrdemServico();
            }
            else {
              $this.showAlert(response.data.description, "danger");
            }
          })
            .catch(function (error) {
              $this.showAlert("Falha ao Criar/Alterar Ordem Servico", "danger");
            });
          // this will be called only after form is valid. You can do api call here to login
        }
      },
      deleteOrdemServicoConfirmed(id) {
        const found = this.ordensServico.find(element => element.id === id);
        this.$bvModal.msgBoxConfirm('Por favor confirme a exclusão da Ordem de Serviço ' + found.id, {
          title: 'Deseja excluir essa Ordem de Serviço ' + found.id + '?',
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
            // console.log(value)
            if (this.boxTwo == true) {
              this.deleteOrdemServico(id)
            } else {
              this.cancelTitle
            }
          })
          .catch(err => {
            // An error occurred
          })
      },
      deleteOrdemServico(id) {
        let $this = this;
        axios.get("https://localhost:44376/OrdemServico/ExcluirOrdemServico", {
          params: { "id": id }
        }).then(response => {
          if (response.data.sucess == true) {
            console.log(response.data)
            $this.showAlert(response.data.description, "success");
            this.getOrdemServico();
          }
          else {
            $this.showAlert(response.data.description, "warning");
          }
        })
          .catch(function (error) {
            $this.showAlert("Falha ao Excluir", "danger");
          });
      },
      resetModal() {
        this.disabledall = false,
        this.ordemServico = {}
        this.tpOperacao = 'Incluir'
        this.disable = true
      },
      getOrdemServico() {
        let $this = this;
        axios.get("https://localhost:44376/OrdemServico/GetOrdemServico", {
        }).then(response => {
          console.log(response.data, 'roc')
          this.ordensServico = response.data
        })
          .catch(function (error) {
            $this.showAlert("Falha ao Carregar Ordem Servico", "danger");
          });
      },

      GetItensOrcamento() {
        let $this = this;
        axios.get("https://localhost:44376/Orcamento/GetItensOrcamento", {
        }).then(response => {
          console.log(response.data)
          this.itensOrcamento = response.data
        })
          .catch(function (error) {
            $this.showAlert("Falha ao Carregar Orçamento", "danger")
          });
      },

      GetItensOrcamentoById(id) {
        let $this = this;
        axios.get("https://localhost:44376/Orcamento/GetItensOrcamentoById", {
          params: { "id": id }
        }).then(response => {
          console.log(response.data)
          this.itensOrcamento = response.data
        })
          .catch(function (error) {
            $this.showAlert("Falha ao Carregar Itens Ordem Serviço", "danger")
          });
      },

      GetOrdemServicoById(id, tpOperacao) {
        let $this = this;
        axios.get("https://localhost:44376/OrdemServico/GetOrdemServicoById", {
          params: { "id": id }
        }).then(response => {
          console.log(response.data)
          this.ordemServico = response.data.ordemServico
          if (tpOperacao == "Editar") {
            this.$bvModal.show("modal-1");
            this.tpOperacao = tpOperacao
            this.disabledall = false
          }
          if (tpOperacao == "Visualizar") {
            this.tpOperacao = tpOperacao;
            this.$bvModal.show("modal-1");
            this.disabledall = true;
          }
        })
          .catch(function (error) {
            $this.showAlert("Falha ao Carregar  Ordem Serviço ", "danger")
          });
      },

      addProduto(produto) {
        produto.idProduto = produto.id;
        produto.quantidade = produto.qntd;
        produto.qntd = null;
        produto.valorItem = produto.valor;
        produto.valortotal = (produto.valor * produto.quantidade);
        this.lstprodutos.push(produto);
        //  this.produto = {};
        // this.showNovoProduto = false;
      },
      removeProduto(index) {
        this.lstprodutos.splice(index, 1);
        //  this.produto = {};
        // this.showNovoProduto = false;
      },

      getFuncionarios() {
        let $this = this;
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
            $this.showAlert("Falha ao Carregar Funcionarios", "danger")
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
            if (response.data[i].pessoa.tipoPessoa == 1) {
              var opt = { value: response.data[i].id, text: response.data[i].pessoa.nome };
            }
            if (response.data[i].pessoa.tipoPessoa == 2) {
              var opt = { value: response.data[i].id, text: response.data[i].pessoa.nomeFantasia };
            }
            this.clientes.push(opt);
          }
         // console.log('lstclientes', this.clientes);

        })
          .catch(function (error) {
            $this.showAlert("Falha ao Carregar Clientes", "danger")
          });
      },
      showAlert(message, variant) {
        // success
        // danger
        // warning
        console.log('teste');
        this.dismissCountDown = this.dismissSecs
        this.variant = variant
        this.txtAlert = message
      },
      countDownChanged(dismissCountDown) {
        this.dismissCountDown = dismissCountDown
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
    },
    computed: {
      Sum() {
        var retorno = (parseFloat(this.ordemServico.maoDeObra) + parseFloat(this.lstprodutos.reduce((Sum, product) => product.valorItem * product.quantidade + Sum, 0))).toFixed(2)
        if (isNaN(retorno)) {
          return 0;
        }
        else {
          return retorno;
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

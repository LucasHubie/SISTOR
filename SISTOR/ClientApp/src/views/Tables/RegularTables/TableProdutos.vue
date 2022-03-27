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
        <h3 class="mb-0 float-left">Produto</h3>
        <base-button v-b-modal.modal-scrollable type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v:onclick="">
          <b-icon icon="question-circle-fill" aria-label="Help"></b-icon>
        </base-button>
        <base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v-on:click="showFiltrar = !showFiltrar">
          <b-icon icon="filter-square-fill" font-scale="1"></b-icon>
          <span class="btn-inner--text">Filtrar</span>
        </base-button>
        <base-button v-on:click="tpOperacao = 'Incluir'; produto = { produto: {codigo:0}}" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
          <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
          <span v-b-modal.modal-1 class="btn-inner--text">Adicionar</span>
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
            <base-button class="float-right" style="background-color: rgb(58 99 167); margin: 0; position: absolute; top: 53%; -ms-transform: translateY(-50%); transform: translateY(-50%);" v-on:click="buscaProduto">
              <span class="btn-inner--text">Filtrar</span>
            </base-button>
          </b-col>

        </b-row>
      </div>

      <el-table class="table-responsive table"
                header-row-class-name="thead-light"
                :data="produtos">

        <el-table-column label="Código"
                         prop="codigo"
                         min-width="75px">
        </el-table-column>
        <el-table-column label="Descrição"
                         prop="descricao"
                         min-width="170px">
        </el-table-column>

        <el-table-column label="Tipo de medida"
                         min-width="150px"
                         prop="tipoMedida">
          <template v-slot="{row}">
            <badge class="badge-dot mr-4" type="">
              <span class="text-black">{{ getTextoSituacao(row.tipoMedida) }}</span>
            </badge>
          </template>
        </el-table-column>
        <el-table-column label="Valor"
                         prop="valor"
                         min-width="100px">
          <template v-slot="{row}">
            <badge class="badge-dot mr-4" type="">
              <span class="text-black">R$ {{ row.valor }}</span>
            </badge>
          </template>
        </el-table-column>
        <el-table-column label="Ações"
                         min-width="170px"
                         prop="">
          <template v-slot="{row}">
            <el-dropdown v-if="false" trigger="click" class="dropdown">
              <base-button size="sm" type="default" style="background-color: rgb(58 99 167); margin-right: .5rem;"><b-icon icon="three-dots" font-scale="1"></b-icon></base-button>

            </el-dropdown>
            <base-button v-on:click="getProdutoById(row.id, 'Visualizar')" v-b-modal.modal-2 size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="eye-fill" font-scale="1"></b-icon> </base-button>
            <base-button v-on:click="getProdutoById(row.id, 'Alterar')" size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="pencil-fill" font-scale="1"></b-icon></base-button>
            <base-button v-on:click="deleteProdutoConfirmed(row.id)" size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="trash-fill" font-scale="1"></b-icon></base-button>

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
          Para inclusão de um novo produto basta clicar sobre o botão "Adicionar" e preencher todos os campos com os dados sobre o mesmo.
          <br />
          <br />
          Ao lado do botão de inclusão, temos o botão "Filtrar" que ao clicado abre uma caixa de pesquisa onde pode-se buscar pela descrição do produto ou pelo código do produto.
          <br />
          <br />
          Um pouco mais abaixo temos uma listagem de todos os produtos já inclusos no sistema, caso esteja em branco, isso se da por não haver nenhum produto incluso até o momento.
          <br />
          <br />
          Dentro dessa listagem sobre cada produto, temos 3 opções em cada um deles. Começando da esquerda para a direita, temos o botão de detalhes, representado pelo ícone do olho,
          onde após clicado será mostrado todos os detalhes do produto.
          <br />
          <br />
          Na sequência temos o botão do meio que se refere ao botão de alteração do produto, representado pelo ícone de lápis. Então clicando sobre ele, será carregado todos os dados
          do produto para alteração.
          <br />
          <br />
          E por último temos o botão de exclusão, representado pelo ícone da lata de lixo. Onde após clicado, abrirá uma tela de confirmação de exclusão, onde terá a opção de confirmar
          ou não a exclusão do produto.
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

      <b-modal id="modal-1" title="Produto" size="lg">
        <b-form @submit.prevent="handleSubmit">
          <h6 class="heading-small text-muted mb-4">Informações do Produto</h6>
          <div class="">
            <b-form-group>

            </b-form-group>
            <b-row>
              <b-col lg="12">

                <base-input type="text"
                            label="Descrição do Produto"
                            placeholder="Produto"
                            v-model="produto.descricao"
                            name="Descrição"
                            required>
                </base-input>
              </b-col>

              <!--<b-col lg="6">

                <base-input type="number"
                            label="Código"
                            placeholder="Código"
                            v-model="produto.codigo"
                            name="Código"
                            required>
                </base-input>
              </b-col>-->
            </b-row>
            <b-row>
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

      <!--modal detail-->
      <b-modal id="modal-2" title="Detalhes do Produto" size="lg">
        <b-form @submit.prevent="handleSubmit">
          <h6 class="heading-small text-muted mb-4">Informações do Produto</h6>
          <div class="">
            <b-form-group>

            </b-form-group>
            <b-row>
              <b-col lg="6">

                <b-form-group label="Descrição do Produto"
                              label-for="produto-input"
                              invalid-feedback="Produto é obrigatório">
                  <b-form-input id="produto-input"
                                placeholder="Descrição do produto"
                                :disabled="disable"
                                v-model="produto.descricao"
                                required></b-form-input>
                </b-form-group>
              </b-col>

              <b-col lg="6">

                <base-input type="number"
                            label="Código"
                            placeholder="Código"
                            :disabled="disable"
                            v-model="produto.codigo"
                            required>
                </base-input>
              </b-col>
            </b-row>
            <b-row>
              <b-col lg="6">
                <label>
                  Tipos de Medida
                </label>
                <b-form-select v-model="produto.tipoMedida" :options="optionsMedidas" :disabled="disable"></b-form-select>

              </b-col>
              <b-col lg="6">
                <base-input type="number"
                            label="Valor em R$"
                            placeholder="Valor do produto"
                            :disabled="disable"
                            v-model="produto.valor">
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
        dismissSecs: 5,
        dismissCountDown: 0,
        showFiltrar: false,
        filtro: { nome: '' },
        disable: false,
        tpOperacao: "Incluir",
        produto: {
          descricao: '',
          codigo: 0,
          tipoMedida: '',
          valor: ''
        },
        optionsMedidas: [
          { value: 1, text: 'KG - Kilos' },
          { value: 2, text: 'M - Metros' },
          { value: 3, text: 'CM - Centímetros' },
          { value: 4, text: 'MM - Milímetros' },
          { value: 5, text: 'L - Litros' }
        ],
      }
    },
    methods: {

      resetModal() {
        
        this.produto.descricao = ''
        this.produto.codigo = 0
        this.produto.valor = ''
        this.produto.tipoMedida = ''
        this.tpOperacao = 'Incluir'
      },

      countDownChanged(dismissCountDown) {
        this.dismissCountDown = dismissCountDown
      },
      showAlert() {
        this.dismissCountDown = this.dismissSecs
      },

      cancelarHelp() {
        this.$nextTick(() => {
          this.$bvModal.hide('modal-scrollable')
        })
      },

      getTextoSituacao: function (tipoMedida) {
        console.log(tipoMedida);
        var retorno = "";
        switch (tipoMedida) {
          case 1:
            retorno = "KG - Kilos";
            break;
          case 2:
            retorno = "M - Metros";
            break;
          case 3:
            retorno = "CM - Centímetros";
            break;
          case 4:
            retorno = "MM - Milímetros";
            break;
          case 5:
            retorno = "L - Litros";
            break;
        }
        return retorno;
      },
      sendForm() {
        console.log(this.produto)
        var url = "https://localhost:44376/Produto/CriarProduto"
        if (this.tpOperacao == "Alterar") {
          url = "https://localhost:44376/Produto/EditarProduto";
        }
        axios.post(url, this.produto).then(response => {
          if (response.data.sucess == true) {
            console.log(response.data)
            //alert(response.data.description)
            this.$bvModal.hide("modal-1")
            this.showAlert()
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
      buscaProduto() {
        var busca = this.filtro.nome
        console.log(this.produtos)
        axios.get("https://localhost:44376/Produto/BuscaProduto", {
          params: { "busca": busca }
        }).then(response => {
          if (busca != "") {
            this.produtos = response.data.produtos
          } else {
            this.getProdutos(1, 10)
          }
        })
          .catch(function (error) {
            alert(error);
          });
      },

      getProdutoById(id, tpOperacao) {
        if (tpOperacao == "Visualizar") {
          this.disable = true
        }
        axios.get("https://localhost:44376/Produto/GetProdutoById", {
          params: { "id": id }
        }).then(response => {
          console.log(response.data)
          this.produto = response.data.produto
          console.log(this.produto)

          if (tpOperacao == "Alterar") {
            this.$bvModal.show("modal-1");
            this.tpOperacao = tpOperacao
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

      deleteProdutoConfirmed(id) {
        const found = this.produtos.find(element => element.id === id);
        this.$bvModal.msgBoxConfirm('Por favor confirme a exclusão do produto '+ found.descricao, {
          title: 'Deseja excluir o produto '+found.descricao+'?',
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
              this.deleteProduto(id)
            } else {
              this.cancelTitle
            }
          })
          .catch(err => {
          })
      },

      deleteProduto(id) {
        axios.get("https://localhost:44376/Produto/ExcluirProduto", {
          params: { "id": id }
        }).then(response => {
          if (response.data.sucess == true) {
            console.log(response.data)
            this.showAlert()
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

<template>
  <div>
    <b-alert :show="dismissCountDown"
             dismissible
             variant="warning"
             @dismissed="dismissCountDown=0"
             @dismiss-count-down="countDownChanged">
      Confirmado com sucesso! <!--{{ dismissCountDown }}-->
    </b-alert>
    <!-- Header -->
    <div class="header bg-gradient-success py-2 py-lg-1 pt-lg-9">
      <b-container class="container">
        <div class="header-body text-center mb-8">
          <b-row class="justify-content-center">
            <b-col xl="5" lg="6" md="8" class="px-5">
              <h1 class="text-white">Criar conta de usuário</h1>
              <!--<p class="text-lead text-white">Use these awesome forms to login or create new account in your project for
            free.</p>-->
            </b-col>
          </b-row>
        </div>
      </b-container>
      <div class="separator separator-bottom separator-skew zindex-100">
        <svg x="0" y="0" viewBox="0 0 2560 100" preserveAspectRatio="none" version="1.1"
             xmlns="http://www.w3.org/2000/svg">
          <polygon class="fill-default" points="2560 0 2560 100 0 100"></polygon>
        </svg>
      </div>
    </div>



    <!-- Page content -->
    <b-container class="mt--8 pb-5">
      <!-- Table -->
      <b-row class="justify-content-center">
        <b-col lg="6" md="8">
          <b-card no-body class="bg-secondary border-0">
            <!--<b-card-header class="bg-transparent pb-5">
            <div class="text-muted text-center mt-2 mb-4"><small>Sign up with</small></div>
            <div class="text-center">
              <a href="#" class="btn btn-neutral btn-icon mr-4">
                <span class="btn-inner--icon"><img src="img/icons/common/github.svg"></span>
                <span class="btn-inner--text">Github</span>
              </a>
              <a href="#" class="btn btn-neutral btn-icon">
                <span class="btn-inner--icon"><img src="img/icons/common/google.svg"></span>
                <span class="btn-inner--text">Google</span>
              </a>
            </div>
          </b-card-header>-->
            <b-card-body class="px-lg-5 py-lg-5">
              <div class="text-center text-muted mb-4">
                <!--<small>Or sign up with credentials</small>-->
              </div>
              <validation-observer v-slot="{handleSubmit}" ref="formValidator">
                <b-form role="form" @submit.prevent="handleSubmit(onSubmit)">
                  <base-input alternative
                              class="mb-3"
                              prepend-icon="ni ni-hat-3"
                              placeholder="Nome"
                              name="Nome"
                              :rules="{required: true}"
                              v-model="model.Nome">

                  </base-input>

                  <base-input alternative
                              class="mb-3"
                              prepend-icon="ni ni-email-83"
                              placeholder="Login"
                              name="Login"
                              :rules="{required: true}"
                              v-model="model.Login">
                  </base-input>

                  <base-input alternative
                              class="mb-3"
                              prepend-icon="ni ni-lock-circle-open"
                              placeholder="Senha"
                              type="password"
                              name="Senha"
                              :rules="{required: true, min: 6}"
                              v-model="model.Senha">
                  </base-input>
                  <div class="text-muted font-italic">
                    <small>
                      <!--força da senha: <span class="text-success font-weight-700">forte</span>-->
                    </small>
                  </div>
                  <b-row class=" my-4">
                    <b-col cols="12">
                      <base-input :rules="{ required: { allowFalse: false } }" name=Privacy Policy>
                        <b-form-checkbox v-model="model.agree">
                          <span class="text-muted"> Concordo com a <a href="#!">Política de Privacidade</a></span>
                        </b-form-checkbox>
                      </base-input>
                    </b-col>
                  </b-row>
                  <div class="text-center">
                    <b-button type="submit" variant="primary" class="mt-4" v:onClick="onSubmit">Criar conta</b-button>
                  </div>
                </b-form>
              </validation-observer>
            </b-card-body>
          </b-card>
        </b-col>
      </b-row>
    </b-container>

  </div>
</template>
<script>

  import axios from 'axios'

  export default {
    /*name: 'register',*/
    data() {
      return {
        dismissSecs: 5,
        dismissCountDown: 0,
        model: {
          Nome: '',
          Login: '',
          Senha: '',
          agree: false
        }
      }
    },
    methods: {
      countDownChanged(dismissCountDown) {
        this.dismissCountDown = dismissCountDown
      },
      showAlert() {
        this.dismissCountDown = this.dismissSecs
      },
      onSubmit() {

        axios.get("https://localhost:44376/Usuario/CriarUsuario", {
          params: this.model
        }).then(response => {
          console.log(response)
          if (response.data.sucess == true) {
            console.log(response.data)
            this.showAlert()
            window.location.href = "#/login"
            this.showAlert()
            //alert(response.data.description)
          }
          else {
            alert(response.data.description)
          }
          
          //alert("login", response.data)
        })
          .catch(function (error) {
            alert(error);
          });
        // this will be called only after form is valid. You can do api call here to login
      }
      , mounted() { }
    }

  };
</script>
<style></style>

<template>
  <div>
    <!-- Header -->
    <div class="header bg-gradient-success py-7 py-lg-8 pt-lg-9">
      <b-container>
        <div class="header-body text-center mb-7">
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
      <b-row class="justify-content-center">
        <b-col lg="5" md="7">
          <b-card no-body class="bg-secondary border-0 mb-0">
            <b-card-header class="bg-transparent pb-5">
              <div class="btn-wrapper text-center">
                <router-link class="" to="/" style=" background-color: #3a63a7; border-radius: 10px; display: inline-block;">
                  <img src="img/brand/logoDefault.png" style="width: 80%; padding-top: 1rem; padding-bottom: 1rem; " class="navbar-brand-img" alt="...">
                </router-link>
              </div>
            </b-card-header>
            <b-card-body class="px-lg-5 py-lg-5">
              <validation-observer v-slot="{handleSubmit}" ref="formValidator">
                <b-form role="form" @submit.prevent="handleSubmit(onSubmit)">
                  <base-input alternative
                              class="mb-3"
                              name="Email"
                              prepend-icon="ni ni-email-83"
                              placeholder="Login"
                              v-model="model.Nome">
                  </base-input>

                  <base-input alternative
                              class="mb-3"
                              name="Password"
                              prepend-icon="ni ni-lock-circle-open"
                              type="password"
                              placeholder="Senha"
                              v-model="model.Senha">
                  </base-input>

        
                  <div class="text-center">
                    <base-button type="primary" native-type="submit" class="my-4" v:onClick="onSubmit">Login</base-button>
                  </div>
                </b-form>
              </validation-observer>
            </b-card-body>
          </b-card>
          <b-row class="mt-3">
            <b-col cols="6">
              <router-link to="/dashboard" class="text-light"><small>Esqueceu sua Senha?</small></router-link>
            </b-col>
            <b-col cols="6">
              <router-link to="/register" class="text-light float-right" ><small>Cadastre-se</small></router-link>
            </b-col>
          </b-row>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>
<script>

  import axios from 'axios'

  export default {
    data() {
      return {
        model: {
          Nome: '',
          Login: '',
          Senha: '',
          rememberMe: false
        }
      };
    },
    methods: {
      onSubmit() {
        axios.get("https://localhost:44376/Home/Login", {
          params: this.model
        }).then(response => {
          console.log(response.data)
          window.location.href = "#/dashboard"
          //alert("login", response.data)
        })
          .catch(function (error) {
            alert(error);
          });
        // this will be called only after form is valid. You can do api call here to login
      }

      //criaUsuario() {
      //  axios.get("https://localhost:44376/Usuario/Create"), {
      //    params: this.model
      //  }).then(response => {
      //    console.log(respose.data)
      //    window.location.href="#/login"
      //  })
      //  .catch(function (error) {
      //    alert(error);
      //  })
      //}
      , mounted() {} //this.onSubmit() }
    }
  }
</script>

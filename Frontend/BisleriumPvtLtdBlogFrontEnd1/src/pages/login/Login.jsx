import { Button, TextInput } from "@mantine/core";
import React, { useEffect, useState } from "react";
import loginUser from "../../services/loginUser";
import { useNavigate } from "react-router";

const Login = () => {

  const navigate = useNavigate();
  const [username, setusername] = useState('')
  const [password, setpassword] = useState('')
  
  async function handleLoginUser(){

    const res = await loginUser(
      {
        email: username,
        password: password
      }
    );

    const data = await res.json();
    console.log("hhhh")
    console.log(data)

    if(res.ok){
      return navigate('/')
    }
  }
  return (
    <div
      className="d-flex w-100 align-items-center justify-content-center"
      style={{
        minHeight: "100vh",
      }}
    >
      <div
        className="w-25"
        style={{
          height: "200px",
        }}
      >
        <div className="d-flex flex-column gap-3">
          <h1 className="display-6 fw-bold text-center">Login</h1>

          <TextInput label="Username" 
          value={username}
          onChange={(e)=>setusername(e.target.value)}
          />

          <TextInput label="Password" type="password"
          value={password}
          onChange={(e)=>setpassword(e.target.value)}
          />

          <Button
          onClick={handleLoginUser}
          >Login</Button>
        </div>
      </div>
    </div>
  );
};

export default Login;

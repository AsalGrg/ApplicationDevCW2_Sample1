import { Button, TextInput } from "@mantine/core";
import React from "react";

const Register = () => {
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
          height: "300px",
        }}
      >
        <div className="d-flex flex-column gap-3">
          <h1 className="display-6 fw-bold text-center">Register</h1>

          <TextInput label="Username" />

          <TextInput label="Email" />

          <TextInput label="Password" type="password"/>

          <Button>Login</Button>
        </div>
      </div>
    </div>
  );
};

export default Register;

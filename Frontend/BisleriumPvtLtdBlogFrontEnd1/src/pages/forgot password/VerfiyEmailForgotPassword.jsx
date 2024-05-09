import React, { useEffect, useState } from "react";
import Loading from "../../components/global/Loading";
import { useNavigate, useParams } from "react-router";
import verify_email_forgot_password from "../../services/verify_email_forgot_password";
import { Button, Text, TextInput } from "@mantine/core";
import { useSearchParams } from "react-router-dom";
import change_forgot_password from "../../services/verify_email_forgot_password";

const VerfiyEmailForgotPassword = () => {
  const [searchParams, setSearchParams] = useSearchParams();
  const [password, setpassword] = useState('')

  const navigate = useNavigate();

  const email = searchParams.get("email");
  let token = searchParams.get("token");

  token = token.replace(/\s+/g, "+");

  console.log(token);

  async function changeForgotPassword() {
    const forgotPasswordData = {
      changedPassword: password,
      email: email,
      token: token
    }
    const res = await change_forgot_password(forgotPasswordData);

    if(res.ok){
      return navigate('/login');
    }
  }

  return (
    <div
      className="d-flex justify-content-center align-items-center"
      style={{
        minHeight: "600px",
      }}
    >
      {token.length > 1 ? (
        <div className="d-flex flex-column gap-3">
          <Text size="lg" fw={700} className="text-center">
            Enter a new password
          </Text>
          <TextInput label="New password" placeholder="Enter new password" 
          onChange={(e)=> setpassword(e.target.value)}
          value={password}
          />

          <Button
          onClick={changeForgotPassword}
          >Enter</Button>
        </div>
      ) : (
        <p className="lead">
          Click on the link sent on <span className="fw-bold">{email}</span> to
          verify{" "}
        </p>
      )}
    </div>
  );
};

export default VerfiyEmailForgotPassword;

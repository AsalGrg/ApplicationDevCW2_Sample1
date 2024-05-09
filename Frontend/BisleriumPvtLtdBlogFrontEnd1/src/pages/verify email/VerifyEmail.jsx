import React, { useEffect } from "react";
import { useNavigate, useParams } from "react-router";

import Loading from "../../components/global/Loading";
import { useSearchParams } from "react-router-dom";
import verify_email from "../../services/verifyEmail";

const VerifyEmail = () => {
    const [searchParams, setSearchParams]= useSearchParams();

    const navigate= useNavigate();


    const email = searchParams.get('email')
    let token = searchParams.get('token')

    token =token.replace(/\s+/g, '+');

    console.log(token);
  
    useEffect(() => {
      async function verifyEmail() {
        if (token.length>5) {
            const data = {
                email: email,
                token: token
            }
            const res = await verify_email(data);

            if(res.ok){
                return navigate('/login');
            }
        }
      }
  
      verifyEmail();
  
    }, []);
  
    return (
      <div
        className="d-flex justify-content-center align-items-center"
        style={{
          minHeight: "600px",
        }}
      >
        {token.length>1 ? (
          <Loading />
        ) : (
          <p className="lead">
            Click on the link sent on <span className="fw-bold">{email}</span> to
            verify{" "}
          </p>
        )}
      </div>
    );
};

export default VerifyEmail;

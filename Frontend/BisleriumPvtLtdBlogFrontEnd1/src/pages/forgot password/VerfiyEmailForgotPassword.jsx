import React, { useEffect } from 'react'
import Loading from '../../components/global/Loading';
import { useParams } from 'react-router';

const VerfiyEmailForgotPassword = () => {
    const { token, email } = useParams();

  console.log(token);

  useEffect(() => {
    function verifyEmail() {
      if (token !== null) {
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
      {token !== "null" ? (
        <Loading />
      ) : (
        <p className="lead">
          Click on the link sent on <span className="fw-bold">{email}</span> to
          verify{" "}
        </p>
      )}
    </div>
  );
}

export default VerfiyEmailForgotPassword
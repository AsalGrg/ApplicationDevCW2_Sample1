import { Button, Text, TextInput } from '@mantine/core'
import React from 'react'

const ForgotPassword = () => {
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
          <h1 className="display-6 fw-bold text-center">Forgot Password</h1>
          <Text c={'dimmed'} className='text-center'>Enter email linked to your account</Text>
          <TextInput label="Email" />
          <Button>Enter</Button>
        </div>
      </div>
    </div>
  )
}

export default ForgotPassword
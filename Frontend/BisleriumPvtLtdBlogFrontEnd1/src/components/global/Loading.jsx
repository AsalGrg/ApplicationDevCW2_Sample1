import React from "react";

const Loading = () => {
  return (
    <div
    className="d-flex align-items-center justify-content-center"
      style={{
        minHeight: "100vh",
      }}
    >
      <div
      style={{
        height: '100px',
      }}
      >
        <div class="spinner-border text-secondary" role="status"
        style={{
            width: '4rem',
            height: '4rem'
        }}
        >
          <span class="visually-hidden">Loading...</span>
        </div>
      </div>
    </div>
  );
};

export default Loading;

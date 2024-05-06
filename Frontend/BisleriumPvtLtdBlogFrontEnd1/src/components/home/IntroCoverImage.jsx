import React from "react";

const IntroCoverImage = () => {
  return (
    <div
      style={{
        height: "350px",
      }}
      className="bg-success"
    >
      <div className="w-100 row justify-content-between px-4 h-100">
        <div className="col-5 d-flex flex-column justify-content-center align-items-start gap-2">
          <p className="display-6 fw-bolder text-light">
            Discover Bislerium Blogs
          </p>
          <p className="lead fw-bold text-light">Dive into Knowledge.</p>
        </div>

        <div className="col-5">
          <img
            src="https://th.bing.com/th/id/OIP.-em1yIEO6UhBQX-IT_VG8QHaEO?w=700&h=400&rs=1&pid=ImgDetMain"
            className="w-100 h-100"
            style={{
                objectFit: 'cover'
            }}
            alt=""
          />
        </div>
      </div>
    </div>
  );
};

export default IntroCoverImage;

import { Avatar, Button, Popover, Text } from "@mantine/core";
import React, { useEffect, useState } from "react";
import { IoIosNotifications } from "react-icons/io";
import EachNotification from "../global/EachNotification";
import { get_user_details } from "../../services/getUserDetails";

const Navbar = () => {
  const [loggedInUserDetails, setloggedInUserDetails] = useState(null);

  useEffect(() => {
    async function getUserDetails(){
      const res= await get_user_details();
      const data = await res.json();

      console.log(data);
      if(res.ok){
        setloggedInUserDetails(data);
      }
    }
    getUserDetails();
  }, [])
  
  return (
    <div
      className="border-bottom border-2"
      style={{
        height: "70px",
      }}
    >
      <div className="w-100 h-100 row align-items-center justify-content-between px-5">
        <div className="col-3 d-flex align-items-center gap-3">
          <p className="btn btn-link mt-3 text-dark">Write</p>
          <p className="btn btn-link mt-3 text-dark">Blogs</p>
        </div>
        <div className="col-3 text-center">
          <h1 className="lead fw-bold">Bislerium Blogs</h1>
        </div>
        <div className="col-3 d-flex align-items-center gap-3">
          {!loggedInUserDetails ? (
            <div className="d-flex gap-3">
              <p className="btn btn-link mt-3 text-dark">Log in</p>
              <Button size="sm" radius={"md"} className="bg-success mt-3">
                Get started
              </Button>
            </div>
          ) : (
            <div
              className="d-flex gap-4"
              style={{
                fontSize: "28px",
              }}
            >
              <Popover width={320} position="bottom" withArrow shadow="md">
                <Popover.Target>
                  <Button
                    c={"dark"}
                    variant="light"
                    style={{
                      fontSize: "28px",
                    }}
                  >
                    <IoIosNotifications />
                  </Button>
                </Popover.Target>
                <Popover.Dropdown>
                  <div className="d-flex flex-column gap-4">
                    <EachNotification />
                    <EachNotification />
                    <EachNotification />
                  </div>
                </Popover.Dropdown>
              </Popover>

              <div className="d-flex gap-2 align-items-center">
                <Avatar size={"md"} />
                <Text size="sm" fw={"600"}>
                  Asal Gurung
                </Text>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default Navbar;

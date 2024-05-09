import { Avatar, Button, Popover, Text } from "@mantine/core";
import React, { useEffect, useState } from "react";
import { IoIosNotifications } from "react-icons/io";
import EachNotification from "../global/EachNotification";
import { get_user_details } from "../../services/getUserDetails";
import { useNavigate } from "react-router";

const Navbar = () => {
  const [loggedInUserDetails, setloggedInUserDetails] = useState(null);
  const navigate = useNavigate();


  useEffect(() => {
    async function getUserDetails(){
      const res= await get_user_details();
      const data = await res.json();

      console.log(data);
      
      if(res.ok){
        if(data.IsAdmin===true){
          window.location.replace('/admin/home');
        }else{
          setloggedInUserDetails(data);
        }
      }
    }
    getUserDetails();
  }, [])
 

  return (
    <div
      className="border-bottom border-2 w-100 h-100"
      style={{
        height: "70px",
      }}
    >
      <div className="w-100 h-100 row align-items-center justify-content-between px-5">
        <div className="col-3 d-flex align-items-center gap-3">
          <p className="btn btn-link mt-3 text-dark"
          onClick={()=> navigate('/blog/new')}
          >Write</p>
        </div>
        <div className="col-3 text-center">
          <h1 className="lead fw-bold">Bislerium Blogs</h1>
        </div>
        <div className="col-3 d-flex align-items-center gap-3">
          {!loggedInUserDetails ? (
            <div className="d-flex gap-3">
              <p className="btn btn-link mt-3 text-dark"
              onClick={()=> navigate('/login')}
              >Log in</p>
              <Button size="sm" radius={"md"} className="bg-success mt-3"
              onClick={()=> navigate('/register')}
              >
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
                    {loggedInUserDetails.allNotifications.map(each =>(
                      <EachNotification  notification={each}/>
                    ))}
                  </div>
                </Popover.Dropdown>
              </Popover>

              <div className="d-flex gap-2 align-items-center"
              onClick={()=> navigate('/profile')}
              >
                <Avatar size={"md"} />
                <Text size="sm" fw={"600"}>
                  {loggedInUserDetails.Username}
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

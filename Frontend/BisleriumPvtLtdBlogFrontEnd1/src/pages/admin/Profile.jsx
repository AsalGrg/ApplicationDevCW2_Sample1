import { Button, Text, TextInput } from "@mantine/core";
import React, { useEffect, useState } from "react";
import { get_user_details } from "../../services/getUserDetails";
import { update_user_profile } from "../../services/updateUserProfile";
import { delete_user_profile } from "../../services/deleteUserDetails";
import { useNavigate } from "react-router";

const Profile = () => {
  const [isEditing, setisEditing] = useState(false);

  const navigate = useNavigate();

  const [userProfileDetails, setuserProfileDetails] = useState(null);

  useEffect(() => {
    async function getUserDetails() {
      const res = await get_user_details();
      const data = await res.json();

      console.log(data);
      if (res.ok) {
        setuserProfileDetails(data);
      }
    }
    getUserDetails();
  }, []);

  async function handleSaveProfileChanges() {
    const updatedData = {
      Id: userProfileDetails.UserId,
      PhoneNumber: userProfileDetails.PhoneNumber,
      Email: userProfileDetails.Email,
      Password: userProfileDetails.Password,
      Username: userProfileDetails.Username,
    };

    const res = await update_user_profile(updatedData);

    if (res.ok) {
      setisEditing(false);
    }
  }

  async function deleteUserProfile() {
    const res = await delete_user_profile(userProfileDetails.UserId);

    if (res.status===400) {
        localStorage.removeItem('token')
        return navigate('/login')
    }
  }
  return (
    <div
      className="d-flex w-100 align-items-center justify-content-center"
      style={{
        minHeight: "100vh",
      }}
    >
      {userProfileDetails !== null ? (
        <div
          className="w-25"
          style={{
            height: "400px",
          }}
        >
          <div className="d-flex flex-column gap-2">
            <h1 className="display-6 fw-bold text-center">User profile</h1>

            <TextInput
              label="Username"
              disabled={!isEditing}
              value={userProfileDetails.Username}
              onChange={(e) =>
                setuserProfileDetails({
                  ...userProfileDetails,
                  Username: e.target.value,
                })
              }
            />

            <TextInput
              label="Email"
              disabled
              value={userProfileDetails.Email}
            />

            <TextInput
              label="Password"
              disabled
              type="password"
              value={userProfileDetails.Password}
            />

            <TextInput
              label="Phone number"
              disabled={!isEditing}
              value={userProfileDetails.PhoneNumber}
              onChange={(e) =>
                setuserProfileDetails({
                  ...userProfileDetails,
                  PhoneNumber: e.target.value,
                })
              }
            />

            {!isEditing ? (
              <div className="d-flex gap-2">
                <Button onClick={() => setisEditing(true)}>Edit</Button>
                <Button color="red"
                onClick={deleteUserProfile}
                >Delete</Button>
              </div>
            ) : (
              <Button onClick={handleSaveProfileChanges}>Save Changes</Button>
            )}
          </div>
        </div>
      ) : null}
    </div>
  );
};

export default Profile;
